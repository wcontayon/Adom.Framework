namespace Adom.CQRS.Abstractions;

/// <summary>
/// Represents the next handler or behavior in the pipeline.
/// </summary>
/// <typeparam name="TResponse">The response type returned by the handler.</typeparam>
/// <returns>A <see cref="ValueTask{TResponse}"/> representing the asynchronous operation.</returns>
#pragma warning disable CA1711 // Delegate suffix is intentional and follows .NET conventions
public delegate ValueTask<TResponse> RequestHandlerDelegate<TResponse>();
#pragma warning restore CA1711
