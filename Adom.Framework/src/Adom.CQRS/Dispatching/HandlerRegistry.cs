using System.Collections.Frozen;
using Adom.CQRS.Internal;

namespace Adom.CQRS.Dispatching;

/// <summary>
/// Registry for handler lookups using FrozenDictionary for zero-allocation O(1) lookups.
/// </summary>
/// <param name="handlers">Dictionary of handlers to freeze.</param>
internal sealed class HandlerRegistry(Dictionary<Type, HandlerDescriptor> handlers)
{
    private readonly FrozenDictionary<Type, HandlerDescriptor> _handlers = (handlers ?? throw new ArgumentNullException(nameof(handlers))).ToFrozenDictionary();

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
