using System.Threading;
using System.Threading.Tasks;

namespace Adom.Framework.AsyncLock
{
    internal sealed class AsyncLock
    {
        internal readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);
        private readonly Releaser _releaser;

        public AsyncLock()
        {
            _releaser = new Releaser(this);
        }

        public Task<Releaser> LockAsync()
        {
            var slim = _semaphore.WaitAsync();
            return slim.IsCompleted ? Task.FromResult<Releaser>(_releaser) :
                 slim.ContinueWith(
                     (_, state) => new Releaser(state as AsyncLock),
                     this, CancellationToken.None,
                     TaskContinuationOptions.ExecuteSynchronously, 
                     TaskScheduler.Default);
            
        }
    }
}
