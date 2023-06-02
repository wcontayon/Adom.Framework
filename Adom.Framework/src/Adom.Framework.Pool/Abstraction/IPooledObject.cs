using System;
namespace Adom.Framework.Pools.Abstraction;

/// <summary>
/// Wrapper interface to easily manage <see cref="IPool{T}"/> item
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPooledObject<T>: IAsyncDisposable, IDisposable
    where T : class
{
    /// <summary>
    /// Gets the underlying <see cref="IPool{T}"/> instance
    /// </summary>
    IPool<T> Pool { get; }

    /// <summary>
    /// Retrieves an pooled <see cref="T"/> from the underlying <see cref="IPool{T}"/>.
    /// </summary>
    /// <returns></returns>
    T Take();
}