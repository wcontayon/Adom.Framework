using System.Collections.Frozen;
using Adom.CQRS.Internal;

namespace Adom.CQRS.Dispatching;

/// <summary>
/// Registry for handler lookups using FrozenDictionary for zero-allocation O(1) lookups.
/// </summary>
internal sealed class HandlerRegistry
{
    private readonly FrozenDictionary<Type, HandlerDescriptor> _handlers;

    /// <summary>
    /// Initializes a new instance of the <see cref="HandlerRegistry"/> class.
    /// </summary>
    /// <param name="handlers">Dictionary of handlers to freeze.</param>
    public HandlerRegistry(Dictionary<Type, HandlerDescriptor> handlers)
    {
        ArgumentNullException.ThrowIfNull(handlers);
        _handlers = handlers.ToFrozenDictionary();
    }

    /// <summary>
    /// Tries to get a handler descriptor for the specified request type.
    /// </summary>
    /// <param name="requestType">The request type.</param>
    /// <param name="descriptor">The handler descriptor if found.</param>
    /// <returns>True if a handler was found; otherwise, false.</returns>
    public bool TryGetHandler(Type requestType, out HandlerDescriptor descriptor)
    {
        return _handlers.TryGetValue(requestType, out descriptor);
    }
}
