using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Adom.Framework.AsyncLock
{
    public readonly struct Releaser : IAsyncDisposable, IDisposable, IEquatable<Releaser>
    {
        private readonly AsyncLock? _asyncLock;

        internal Releaser(AsyncLock asyncLock)
        {
            _asyncLock = asyncLock;
        }

        public void Dispose()
        {
            Debug.Assert(_asyncLock != null);
            _asyncLock.Release();
        }

        public async ValueTask DisposeAsync()
        {
            this.Dispose();
            await ValueTask.CompletedTask.ConfigureAwait(false);
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is Releaser))
                return false;

            return Equals((Releaser)obj);
        }

        public bool Equals(Releaser other) => this._asyncLock!._semaphore.Equals(other._asyncLock!._semaphore);

        public override int GetHashCode()
        {
            return HashCode.Combine(_asyncLock);
        }

        public static bool operator ==(Releaser point1, Releaser point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(Releaser point1, Releaser point2)
        {
            return !point1.Equals(point2);
        }
    }
}
