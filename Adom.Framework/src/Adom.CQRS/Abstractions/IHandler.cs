namespace Adom.CQRS.Abstractions;

/// <summary>
/// Handles a specific request type and produces a response.
/// </summary>
/// <typeparam name="TRequest">The type of request to handle.</typeparam>
/// <typeparam name="TResponse">The type of response to return.</typeparam>
public interface IHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handles the request asynchronously.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="ValueTask{TResponse}"/> representing the asynchronous operation.</returns>
    ValueTask<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}

/// <summary>
/// Handles a request that returns no value.
/// </summary>
/// <typeparam name="TRequest">The type of request to handle.</typeparam>
public interface IHandler<in TRequest> : IHandler<TRequest, Unit>
    where TRequest : IRequest
{
}
