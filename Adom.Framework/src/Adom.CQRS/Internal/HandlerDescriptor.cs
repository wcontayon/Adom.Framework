namespace Adom.CQRS.Internal;

/// <summary>
/// Internal value type storing handler metadata for efficient lookup.
/// </summary>
internal readonly struct HandlerDescriptor
{
    /// <summary>
    /// Gets the concrete handler type.
    /// </summary>
    public Type HandlerType { get; init; }

    /// <summary>
    /// Gets the request type this handler processes.
    /// </summary>
    public Type RequestType { get; init; }

    /// <summary>
    /// Gets the response type the handler returns.
    /// </summary>
    public Type ResponseType { get; init; }

    /// <summary>
    /// Gets the compiled factory delegate for creating handler instances.
    /// </summary>
    public Func<IServiceProvider, object> Factory { get; init; }
}
