﻿using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Adom.Framework.AsyncLock
{
    [SuppressMessage("Performance", "CA1815", Justification = "We do not need the override Equals")]
    [StructLayout(LayoutKind.Auto)]
    public readonly struct Releaser : IAsyncDisposable, IDisposable
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
    }
}
