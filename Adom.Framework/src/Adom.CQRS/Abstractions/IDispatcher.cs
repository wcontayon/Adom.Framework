namespace Adom.CQRS.Abstractions;

/// <summary>
/// Dispatches requests to their corresponding handlers.
/// </summary>
public interface IDispatcher
{
    /// <summary>
    /// Dispatches a request and returns the response.
    /// </summary>
    /// <typeparam name="TRequest">The type of request.</typeparam>
    /// <typeparam name="TResponse">The type of response.</typeparam>
    /// <param name="request">The request to dispatch.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="ValueTask{TResponse}"/> representing the asynchronous operation.</returns>
    ValueTask<TResponse> DispatchAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>;

    /// <summary>
    /// Dispatches a request that returns no value.
    /// </summary>
    /// <typeparam name="TRequest">The type of request.</typeparam>
    /// <param name="request">The request to dispatch.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask DispatchAsync<TRequest>(
        TRequest request,
        CancellationToken cancellationToken = default)
        where TRequest : IRequest;
}
