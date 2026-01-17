
using Adom.Framework.Pools.Abstraction;

namespace Adom.Framework.Pools;

/// <summary>
/// Wrapper for a pooled object that allows easily retrieving and returning the item to the pool
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class PooledObject<T> : IPooledObject<T>
    where T: class
{
    private readonly IPool<T> _pool;
    private T _value;
    private bool _isDiposed;
    private readonly bool _releasePooledItemOnDispose;

    /// <summary>
    /// Create a <see cref="PooledObject{T}"/> wrapper with a new instance to pool
    /// </summary>
    /// <param name="policy"></param>
    /// <param name="value"></param>
    /// <param name="releasePooledItemOnDispose">Release the pooled item on dispose, or directly dispose the current <see cref="IPooledObject{T}"/> instance.</param>
    public PooledObject(PoolPolicy<T> policy, T value, bool releasePooledItemOnDispose = false)
    {
        ArgumentNullException.ThrowIfNull(policy);
        ArgumentNullException.ThrowIfNull(value);

        _releasePooledItemOnDispose = releasePooledItemOnDispose;
        _pool = new Pool<T>(policy);
        _value = value;
        _isDiposed = false;
    }

    /// <inheritdoc />
    public IPool<T> Pool { get => _pool; }

    /// <inheritdoc />
    public T Take()
    {
        if (_value == null)
        {
            _value = _pool.GetObject();
        }

        return _value;
    }

    public void Dispose()
    {
        if (_releasePooledItemOnDispose)
        {
            // Instead of dispose the wrapper, we just release the pooled item
            try
            {
                if (_value != null)
                {
                    _pool.Release(_value);
                    _value = null!;
                }
            }
            catch (ObjectDisposedException)
            {
                // An error occured we release the PooledObject wrapper
                DisposeMe();
            }
        }
        else DisposeMe();
    }

    public async ValueTask DisposeAsync()
    {
        if (_releasePooledItemOnDispose)
        {
            // Instead of dispose the wrapper, we just release the pooled item
            try
            {
                if (_value != null)
                {
                    _pool.Release(_value);
                    _value = null!;
                }
            }
            catch (ObjectDisposedException)
            {
                // An error occured we release the PooledObject wrapper
                await DisposeMeAsync().ConfigureAwait(false);
            }
        }
        else await DisposeMeAsync().ConfigureAwait(false);
    }

    #region Private Disposable methods

    private void DisposeMe()
    {
        if (_isDiposed) return;
        _pool.Dispose();
        _isDiposed = true;
    }

    private async ValueTask DisposeMeAsync()
    {
        if (_isDiposed) return;
        await _pool.DisposeAsync().ConfigureAwait(false);
        _isDiposed = true;
    }

    #endregion
}
