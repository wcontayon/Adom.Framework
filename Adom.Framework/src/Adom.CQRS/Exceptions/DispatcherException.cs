namespace Adom.CQRS.Exceptions;

/// <summary>
/// Thrown when an error occurs during request dispatch.
/// </summary>
#pragma warning disable CA1032 // Custom exception with specific constructor signature
public sealed class DispatcherException : Exception
#pragma warning restore CA1032
{
    /// <summary>
    /// Gets the request type being dispatched when the error occurred.
    /// </summary>
    public Type RequestType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DispatcherException"/> class.
    /// </summary>
    /// <param name="requestType">The request type being dispatched.</param>
    /// <param name="innerException">The exception that caused the dispatcher error.</param>
    public DispatcherException(Type requestType, Exception innerException)
        : base($"Error dispatching request of type '{(requestType ?? throw new ArgumentNullException(nameof(requestType))).FullName}'.", innerException)
    {
        RequestType = requestType;
    }
}
