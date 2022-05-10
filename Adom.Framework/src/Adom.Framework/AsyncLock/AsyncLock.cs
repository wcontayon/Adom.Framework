using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Adom.Framework.AsyncLock
{
    [SuppressMessage("Microsoft.Naming", "CA1724", Justification = "We keep the name asyncLock")]
    public sealed class AsyncLock
    {
        private readonly bool _allowInliningAwaiters;
        internal readonly Action<object> _onCancellationRequestHandled;
        private readonly Queue<AsyncLockWaiterCompletionSource> _awaiters = new();
        private bool _signaled = true;

        public AsyncLock() : this(allowInliningAwaiters: false)
        {
        }

        public AsyncLock(bool allowInliningAwaiters)
        {
            _allowInliningAwaiters = allowInliningAwaiters;
            _onCancellationRequestHandled = OnCancellationRequest;
        }

        public void Release()
        {
            AsyncLockWaiterCompletionSource? releaseObject = default;
            lock (_awaiters)
            {
                if (_awaiters.Count > 0)
                {
                    releaseObject = _awaiters.Dequeue();
                }
                else if (!_signaled)
                {
                    _signaled = true;
                }
            }

#pragma warning disable CA1508
            if (releaseObject != null && releaseObject != default)
#pragma warning restore CA1508
            {
                releaseObject.CancellationTokenRegistration.Dispose();
                releaseObject.TrySetResult(new Releaser(this));
            }
        }

        public ValueTask<Releaser> LockAsync() => LockAsync(CancellationToken.None);

        public ValueTask<Releaser> LockAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return ValueTask.FromCanceled<Releaser>(cancellationToken);
            }

            lock (_awaiters)
            {
                if (_signaled)
                {
                    _signaled = false;
                    return ValueTask.FromResult<Releaser>(new Releaser(this));
                }
                else
                {
                    var waiter = new AsyncLockWaiterCompletionSource(this, _allowInliningAwaiters, cancellationToken);
                    if (cancellationToken.IsCancellationRequested)
                    {
                        waiter.TrySetCanceled(cancellationToken);
                    }
                    else
                    {
                        _awaiters.Enqueue(waiter);
                    }

#pragma warning disable CA1849
                    return ValueTask.FromResult<Releaser>(waiter.Task.Result);
#pragma warning restore CA1849
                }
            }
        }

        public bool TryLock(out Releaser releaser)
        {
            if (_signaled)
            {
                lock (_awaiters)
                {
                    if (_signaled)
                    {
                        _signaled = false;
                        releaser = new Releaser(this);
                        return true;
                    }
                }
            }

            releaser = new Releaser();
            return false;
        }

        // Copied from https://github.com/meziantou/Meziantou.Framework/blob/67816c4e5f7a2b6116e24a3d8a9a77348acb5efb/src/Meziantou.Framework/Threading/AsyncLock.cs
        private void OnCancellationRequest(object state)
        {
            var tcs = (AsyncLockWaiterCompletionSource)state;
            bool removed;
            lock (_awaiters)
            {
                removed = RemoveMidQueue(_awaiters, tcs);
            }

            // We only cancel the task if we removed it from the queue.
            // If it wasn't in the queue, either it has already been signaled
            // or it hasn't even been added to the queue yet. If the latter,
            // the Task will be canceled later so long as the signal hasn't been awarded
            // to this Task yet.
            if (removed)
            {
                tcs.TrySetCanceled(tcs.CancellationToken);
            }
        }

        // Copied from https://github.com/meziantou/Meziantou.Framework/blob/67816c4e5f7a2b6116e24a3d8a9a77348acb5efb/src/Meziantou.Framework/Threading/AsyncLock.cs
        private static bool RemoveMidQueue<T>(Queue<T> queue, T valueToRemove)
            where T : class
        {
            var originalCount = queue.Count;
            var dequeueCounter = 0;
            var found = false;
            while (dequeueCounter < originalCount)
            {
                dequeueCounter++;
                var dequeued = queue.Dequeue();
                if (!found && dequeued == valueToRemove)
                { // only find 1 match
                    found = true;
                }
                else
                {
                    queue.Enqueue(dequeued);
                }
            }

            return found;
        }
    }

    sealed class AsyncLockWaiterCompletionSource : TaskCompletionSource<Releaser>
    {
        internal AsyncLockWaiterCompletionSource(AsyncLock parent, bool allowInligningContinuations, CancellationToken cancellationToken)
            : base(allowInligningContinuations ? TaskCreationOptions.None : TaskCreationOptions.RunContinuationsAsynchronously)
        {
            CancellationToken = cancellationToken;
            CancellationTokenRegistration = cancellationToken.Register(parent._onCancellationRequestHandled!, this);
        }

        internal CancellationToken CancellationToken { get; }
        internal CancellationTokenRegistration CancellationTokenRegistration { get; }
    }
}
