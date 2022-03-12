using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Adom.Framework.AsyncLock
{
    [SuppressMessage("Microsoft.Naming", "CA1724")]
    public sealed class AsyncLock : IDisposable, IAsyncDisposable
    {
        internal readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private readonly Releaser _releaser;
        private bool _disposed;

        public AsyncLock()
        {
            _releaser = new Releaser(this);
            _disposed = false;
        }

        public void Release()
        {
            if (!_disposed)
            {
                _semaphore.Release();
                _semaphore.Dispose();
                _disposed = true;
            }
        }

        public ValueTask<Releaser> LockAsync() => LockAsync(CancellationToken.None);

        public ValueTask<Releaser> LockAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return ValueTask.FromCanceled<Releaser>(cancellationToken);
            }

            var slim = _semaphore.WaitAsync(cancellationToken);
            if (slim.IsCompleted)
            {
                return ValueTask.FromResult<Releaser>(_releaser);
            }
            else
            {
                Func<Task, object?, Releaser> releaseAction = (_, state) => new Releaser((state as AsyncLock)!);
                var task = slim.ContinueWith(releaseAction, this, cancellationToken, TaskContinuationOptions.None, TaskScheduler.Default);
                return new ValueTask<Releaser>(task);
            }
        }

        public async ValueTask DisposeAsync()
        {
            Release();
            await ValueTask.CompletedTask.ConfigureAwait(false);
        }

        public void Dispose()
        {
            Release();
        }
    }
}
