
using System.Reflection;

namespace Adom.Framework.Pool.Abstraction;

public abstract class PoolBase<T> : IPool<T>
{
    private readonly PoolPolicy<T> _policy;
    private PropertyInfo? _pooledObjectValueProperty;
    private bool _isDisposed;

    protected PoolBase(PoolPolicy<T> policy)
    {
        ArgumentNullException.ThrowIfNull(nameof(policy));
        ArgumentNullException.ThrowIfNull(nameof(policy.ObjectPooledInitialization), "ObjectPooledInitialization");

        _policy = policy;
        _isDisposed = false;
    }

    public bool IsDisposed { get => _isDisposed; }

    #region Dispose

    public void Dispose()
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

    #endregion

    #region PoolBase methods
    
    /// <summary>
    /// Gets the <see cref="PoolPolicy{T}"/>
    /// </summary>
    protected PoolPolicy<T> Policy => _policy;

    /// <summary>
    /// Ensure that current instance of <see cref="PoolBase{T}"/> is not disposed.
    /// </summary>
    protected void EnsureNotDispose() => ObjectDisposedException.ThrowIf(_isDisposed, this);

    #endregion

    protected abstract void Dispose(bool disposing);

    /// <inheritdoc />
    public abstract T GetObject();

    /// <inheritdoc />
    public abstract void IncreasePoolSize(int increment);

    public abstract void Release();
}