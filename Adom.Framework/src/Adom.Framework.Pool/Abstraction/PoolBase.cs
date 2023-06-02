
namespace Adom.Framework.Pools.Abstraction;

public abstract class PoolBase<T> : IPool<T>
    where T : class
{
    private readonly PoolPolicy<T> _policy;
    private readonly bool _isPooledTypeDisposable;
    private readonly bool _isDisposableType;
    private readonly bool _isAsyncDispoableType;
    private bool _isDisposed;

    protected PoolBase(PoolPolicy<T> policy)
    {
        ArgumentNullException.ThrowIfNull(nameof(policy));
        ArgumentNullException.ThrowIfNull(nameof(policy.ObjectPooledInitialization), "ObjectPooledInitialization");
        _isDisposableType = typeof(IDisposable).IsAssignableFrom(typeof(T));
        _isAsyncDispoableType = typeof(IAsyncDisposable).IsAssignableFrom(typeof(T));
        _isPooledTypeDisposable = _isDisposableType || _isAsyncDispoableType;

        _policy = policy;
        _isDisposed = false;
    }

    public bool IsDisposed { get => _isDisposed; }

    #region Dispose

#pragma warning disable CA1063 // Implémenter IDisposable correctement
    public void Dispose()
#pragma warning restore CA1063 // Implémenter IDisposable correctement
    {
        if (_isDisposed) return;
        if (!_isDisposed)
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        _isDisposed = true;
    }

    public ValueTask DisposeAsync()
    {
        if (_isDisposed) return ValueTask.CompletedTask;
        if (!_isDisposed)
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        _isDisposed = true;
        return ValueTask.CompletedTask;
    }

    protected void SafeDispose(T pooledObject)
    {
        if (_isPooledTypeDisposable)
        {
            ArgumentNullException.ThrowIfNull(pooledObject);

            if (_isAsyncDispoableType)
            {
                var disposed = ((IAsyncDisposable)pooledObject)!.DisposeAsync().ConfigureAwait(true);
                return;
            }

            if (_isDisposableType)
            {
                ((IDisposable)pooledObject)!.Dispose();
                return;
            }
        }
    }

    #endregion

    #region PoolBase methods
    
    /// <summary>
    /// Gets the <see cref="PoolPolicy{T}"/>
    /// </summary>
    protected PoolPolicy<T> Policy => _policy;

    /// <summary>
    /// Ensure that current instance of <see cref="PoolBase{T}"/> is not disposed.
    /// </summary>
    protected void EnsureNotDisposed() => ObjectDisposedException.ThrowIf(_isDisposed, this);

    #endregion

    protected abstract void Dispose(bool disposing);

    /// <inheritdoc />
    public abstract T GetObject();

    /// <inheritdoc />
    public abstract void IncreasePoolSize(int increment);

    /// <inheritdoc />
    public abstract void Release(T item);
}