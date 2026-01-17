using System.Runtime.CompilerServices;
using Adom.CQRS.Abstractions;
using Adom.CQRS.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Adom.CQRS.Dispatching;

/// <summary>
/// Core dispatcher implementation that routes requests to handlers through the pipeline.
/// </summary>
/// <param name="serviceProvider">The service provider for resolving handlers and behaviors.</param>
/// <param name="handlerRegistry">The handler registry for fast lookups.</param>
#pragma warning disable CA1812 // Class is instantiated via DI
internal sealed class Dispatcher(IServiceProvider serviceProvider, HandlerRegistry handlerRegistry) : IDispatcher
#pragma warning restore CA1812
{
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    private readonly HandlerRegistry _handlerRegistry = handlerRegistry ?? throw new ArgumentNullException(nameof(handlerRegistry));

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ValueTask<TResponse> DispatchAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>
    {
        if (request == null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(request));
        }

        var requestType = typeof(TRequest);

        if (!_handlerRegistry.TryGetHandler(requestType, out var descriptor))
        {
            ThrowHelper.ThrowHandlerNotFoundException(requestType);
        }

        // Resolve handler
        var handler = (IHandler<TRequest, TResponse>)descriptor.Factory(_serviceProvider);

        // Resolve behaviors (if any)
        var behaviors = _serviceProvider.GetServices<IPipelineBehavior<TRequest, TResponse>>();

        // Build pipeline
        var pipeline = PipelineBuilder.Build(behaviors, handler, request, cancellationToken);

        // Execute pipeline
        return pipeline();
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async ValueTask DispatchAsync<TRequest>(
        TRequest request,
        CancellationToken cancellationToken = default)
        where TRequest : IRequest
    {
        await DispatchAsync<TRequest, Unit>(request, cancellationToken).ConfigureAwait(false);
    }
}
