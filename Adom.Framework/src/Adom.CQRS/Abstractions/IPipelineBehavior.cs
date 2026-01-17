namespace Adom.CQRS.Abstractions;

/// <summary>
/// Pipeline behavior that wraps handler execution.
/// </summary>
/// <typeparam name="TRequest">The type of request.</typeparam>
/// <typeparam name="TResponse">The type of response.</typeparam>
public interface IPipelineBehavior<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handles the request, optionally delegating to the next behavior.
    /// </summary>
    /// <param name="request">The request being handled.</param>
    /// <param name="nextHandler">The next behavior or handler in the pipeline.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="ValueTask{TResponse}"/> representing the asynchronous operation.</returns>
    ValueTask<TResponse> HandleAsync(
        TRequest request,
        RequestHandlerDelegate<TResponse> nextHandler,
        CancellationToken cancellationToken = default);
}
