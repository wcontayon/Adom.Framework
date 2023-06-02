
using System.Collections.Concurrent;
using Adom.Framework.Pools.Abstraction;

namespace Adom.Framework.Pools;

public class Pool<T> : PoolBase<T>
    where T : class
{
    private ConcurrentBag<T> _pooledObjects;

    public Pool(PoolPolicy<T> policy) : base(policy)
    {
        _pooledObjects = new ConcurrentBag<T>();
    }

    #region Dispose

    /// <summary>
    /// Dispose the current instance of <see cref="Pool{T}"/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override void Dispose(bool disposing)
    {
        EnsureNotDisposed();

        if (disposing)
        {
            while (!_pooledObjects.IsEmpty)
            {
                if (_pooledObjects.TryTake(out var item))
                {
                    SafeDispose(item);
                }
            }
        }
    }

    #endregion

    /// <inheritdoc />
    public override T GetObject()
    {
        // Check that our list of pooled object is not empty
        T? item;

        if (_pooledObjects.IsEmpty)
        {
            // Create a new object
            item = InstanciateItem();
        }
        else
        {
            this._pooledObjects.TryTake(out item);
        }

        return item;
    }

    /// <inheritdoc />
    public override void IncreasePoolSize(int increment)
    {
#if NET8_0
        ArgumentOutOfRangeException.ThrowIfNegative(increment);
#else
        if (increment < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(increment));
        }
#endif
        for (int i = 0; i < increment; i++)
        {
            EnsureMaxSizeNotReached();

            this._pooledObjects.Add(InstanciateItem());
        }
    }

    /// <inheritdoc />
    public override void Release(T item)
    {
        ArgumentNullException.ThrowIfNull(item);
        EnsureMaxSizeNotReached();

        this._pooledObjects.Add(item);
    }

    /// <summary>
    /// Check if maximum size of the pool is not reached
    /// </summary>
    /// <exception cref="PoolException"></exception>
    private void EnsureMaxSizeNotReached()
    {
        if (this._pooledObjects.Count == this.Policy.MaxPoolSize
            && this.Policy.RaiseErrorOnMaximumSizeReached)
        {
            throw new PoolException(PoolException.MSGPOOLSIZEREACHED);
        }
    }

    private T InstanciateItem()
        => this.Policy.ObjectPooledInitialization == null ?
            default(T) :
            this.Policy.ObjectPooledInitialization.Invoke();
}

