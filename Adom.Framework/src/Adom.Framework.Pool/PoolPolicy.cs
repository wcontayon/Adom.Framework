
namespace Adom.Framework.Pools;

/// <summary>
/// Defines policy to use in order to create pool of <see cref="T"/> objects
/// </summary>
public class PoolPolicy<T>
{
    /// <summary>
    /// Gets or Sets the method to create a <see cref="T"/> object.
    /// </summary>
    public Func<T>? ObjectPooledInitialization { get; set; }

    /// <summary>
    /// Gets or Sets the option that decide if we need to reinitialize the existing object affected to a request.
    /// If this option is true, the
    /// </summary>
    public bool? ReInitializePooledObjectOnAffectation { get; set; } = false;

    /// <summary>
    /// Gets or Sets the option that decide if we need destroy a released pooled object when the maximum size is reached.
    /// </summary>
    public bool RaiseErrorOnMaximumSizeReached { get; set; }

    /// <summary>
    /// Gets or Sets the maximum number of items in the pool
    /// </summary>
    public int MaxPoolSize { get; set; } = 1;
}

public static class PoolPolicy
{
    public static PoolPolicy<T> DefaultPolicy<T>(
        Func<T> creationFactory = null!,
        bool reinitializePooledItemOnAffectation = false,
        bool raiseErrorOnMaximumSizeReached = false,
        int poolSize = 0)
        => new PoolPolicy<T>()
        {
            ObjectPooledInitialization = creationFactory,
            ReInitializePooledObjectOnAffectation = reinitializePooledItemOnAffectation,
            RaiseErrorOnMaximumSizeReached = raiseErrorOnMaximumSizeReached,
            MaxPoolSize = poolSize == 0 ? Environment.ProcessorCount * 2 : poolSize
        };
}
