using Adom.CQRS.Abstractions;

namespace Adom.CQRS.Dispatching;

/// <summary>
/// Builds a pipeline chain of behaviors and the final handler.
/// </summary>
internal static class PipelineBuilder
{
    /// <summary>
    /// Builds a pipeline of behaviors wrapping the handler.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <param name="behaviors">The pipeline behaviors to apply.</param>
    /// <param name="handler">The final handler to execute.</param>
    /// <param name="request">The request being processed.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A delegate representing the complete pipeline.</returns>
    public static RequestHandlerDelegate<TResponse> Build<TRequest, TResponse>(
        IEnumerable<IPipelineBehavior<TRequest, TResponse>> behaviors,
        IHandler<TRequest, TResponse> handler,
        TRequest request,
        CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>
    {
        ArgumentNullException.ThrowIfNull(handler);
        ArgumentNullException.ThrowIfNull(request);

        // Build the pipeline in reverse order (handler first, then behaviors)
        RequestHandlerDelegate<TResponse> pipeline = () => handler.HandleAsync(request, cancellationToken);

        // Wrap each behavior around the pipeline
        if (behaviors != null)
        {
            foreach (var behavior in behaviors.Reverse())
            {
                var currentPipeline = pipeline;
                var currentBehavior = behavior;
                pipeline = () => currentBehavior.HandleAsync(request, currentPipeline, cancellationToken);
            }
        }

        return pipeline;
    }
}
