
namespace Adom.Framework.Pools.Abstraction;

/// <summary>
/// Interface for object pool
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPool<T> : IAsyncDisposable, IDisposable
    where T : class
{
    /// <summary>
    /// Gets an object from the pool.
    /// If the pool is empty a new item is created, otherwise an existing object is returned.
    /// </summary>
    /// <returns></returns>
    T GetObject();

    /// <summary>
    /// Frees a pooled object and add it to the unused object list.
    /// If the maximum size of pooled object is reached, nothing is 
    /// </summary>
    void Release(T item);

    /// <summary>
    /// Expand the maximum pool size by creating new pooled object
    /// </summary>
    /// <param name="increment"></param>
    void IncreasePoolSize(int increment);

    /// <summary>
    /// Returns true of the <see cref="IDisposable.Dispose"/> or <see cref="IAsyncDisposable.DisposeAsync"/> method has been called on this instance.
    /// </summary>
    bool IsDisposed { get; }
}