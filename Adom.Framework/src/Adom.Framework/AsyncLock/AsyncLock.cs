using System;
using System.Threading;
using System.Threading.Tasks;

namespace Adom.Framework.AsyncLock
{
    internal sealed class AsyncLock : IDisposable
    {
        internal readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private readonly Releaser _releaser;
        internal bool _disposed;

        public AsyncLock()
        {
            _releaser = new Releaser(this);
            _disposed = true;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _semaphore.Release();
                _semaphore.Dispose();
            }
        }

        public Task<Releaser> LockAsync()
        {
            var slim = _semaphore.WaitAsync();
            return slim.IsCompleted ? Task.FromResult<Releaser>(_releaser) :
                 slim.ContinueWith(
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                     (_, state) => new Releaser(asyncLock: state as AsyncLock),
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.
                     this, CancellationToken.None,
                     TaskContinuationOptions.ExecuteSynchronously, 
                     TaskScheduler.Default);
            
        }
    }
}
