using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Adom.Framework.AsyncLock
{
    internal struct Releaser : IAsyncDisposable
    {
        private readonly AsyncLock _asyncLock;
        private bool _disposed;

        internal Releaser(AsyncLock asyncLock)
        {
            _asyncLock = asyncLock;
            _disposed = false;
        }

        public void Dispose()
        {
            Debug.Assert(_asyncLock != null);
            Debug.Assert(!_disposed);
            Debug.Assert(_asyncLock._semaphore != null);
            _asyncLock._semaphore.Release();
            _asyncLock._semaphore.Dispose();
            _disposed = true;
            _asyncLock._disposed = true;
        }

        public async ValueTask DisposeAsync()
        {
            this.Dispose();
            await ValueTask.CompletedTask.ConfigureAwait(false);
        }
    }
}
