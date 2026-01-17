namespace Adom.CQRS.Exceptions;

/// <summary>
/// Thrown when no handler is registered for a request type.
/// </summary>
#pragma warning disable CA1032 // Custom exception with specific constructor signature
public sealed class HandlerNotFoundException : Exception
#pragma warning restore CA1032
{
    /// <summary>
    /// Gets the request type that has no registered handler.
    /// </summary>
    public Type RequestType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HandlerNotFoundException"/> class.
    /// </summary>
    /// <param name="requestType">The request type that has no registered handler.</param>
    public HandlerNotFoundException(Type requestType)
        : base($"No handler registered for request type '{(requestType ?? throw new ArgumentNullException(nameof(requestType))).FullName}'.")
    {
        RequestType = requestType;
    }
}
