using Adom.CQRS;
using Adom.CQRS.Abstractions;
using Adom.CQRS.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Adom.CQRS.Tests;

public class PipelineBehaviorTests
{
    [Fact]
    public async Task DispatchAsync_WithSingleBehavior_ShouldExecuteBehavior()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder
            .ScanAssemblyContaining<PipelineBehaviorTests>());

        // Register behavior manually since AddBehavior expects closed generic
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var command = new BehaviorTestCommand("test");

        // Act
        var result = await dispatcher.DispatchAsync<BehaviorTestCommand, string>(command);

        // Assert
        result.Should().Be("[LOGGED] behavior-test: test");
    }

    [Fact]
    public async Task DispatchAsync_WithMultipleBehaviors_ShouldExecuteInOrder()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder
            .ScanAssemblyContaining<PipelineBehaviorTests>());

        // Register behaviors manually
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FirstBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(SecondBehavior<,>));

        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var command = new OrderTestCommand();

        // Act
        var result = await dispatcher.DispatchAsync<OrderTestCommand, string>(command);

        // Assert
        result.Should().Be("[FIRST][SECOND]HANDLED[/SECOND][/FIRST]");
    }

    [Fact]
    public async Task DispatchAsync_WithNoBehaviors_ShouldExecuteHandlerDirectly()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<PipelineBehaviorTests>());

        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var command = new BehaviorTestCommand("direct");

        // Act
        var result = await dispatcher.DispatchAsync<BehaviorTestCommand, string>(command);

        // Assert
        result.Should().Be("behavior-test: direct");
    }

    [Fact]
    public async Task DispatchAsync_WithBehaviorThatShortCircuits_ShouldNotCallHandler()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder
            .ScanAssemblyContaining<PipelineBehaviorTests>());

        // Register behavior manually
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ShortCircuitBehavior<,>));

        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var command = new ShortCircuitCommand();

        // Act
        var result = await dispatcher.DispatchAsync<ShortCircuitCommand, string>(command);

        // Assert
        result.Should().Be("SHORT_CIRCUITED");
    }

    [Fact]
    public async Task DispatchAsync_WithBehaviorThatThrows_ShouldPropagateException()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder
            .ScanAssemblyContaining<PipelineBehaviorTests>());

        // Register behavior manually
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ThrowingBehavior<,>));

        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var command = new BehaviorTestCommand("error");

        // Act
        Func<Task> act = async () => await dispatcher.DispatchAsync<BehaviorTestCommand, string>(command);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Behavior intentionally threw");
    }
}

// Test commands and handlers
public record BehaviorTestCommand(string Value) : IRequest<string>;

public class BehaviorTestCommandHandler : IHandler<BehaviorTestCommand, string>
{
    public ValueTask<string> HandleAsync(BehaviorTestCommand request, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult($"behavior-test: {request.Value}");
    }
}

public record OrderTestCommand : IRequest<string>;

public class OrderTestCommandHandler : IHandler<OrderTestCommand, string>
{
    public ValueTask<string> HandleAsync(OrderTestCommand request, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult("HANDLED");
    }
}

public record ShortCircuitCommand : IRequest<string>;

public class ShortCircuitCommandHandler : IHandler<ShortCircuitCommand, string>
{
    public ValueTask<string> HandleAsync(ShortCircuitCommand request, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult("HANDLER_CALLED");
    }
}

// Test behaviors
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async ValueTask<TResponse> HandleAsync(
        TRequest request,
        RequestHandlerDelegate<TResponse> nextHandler,
        CancellationToken cancellationToken = default)
    {
        var response = await nextHandler();

        // Wrap the response
        if (response is string str)
        {
            return (TResponse)(object)$"[LOGGED] {str}";
        }

        return response;
    }
}

public class FirstBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async ValueTask<TResponse> HandleAsync(
        TRequest request,
        RequestHandlerDelegate<TResponse> nextHandler,
        CancellationToken cancellationToken = default)
    {
        var response = await nextHandler();

        if (response is string str)
        {
            return (TResponse)(object)$"[FIRST]{str}[/FIRST]";
        }

        return response;
    }
}

public class SecondBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async ValueTask<TResponse> HandleAsync(
        TRequest request,
        RequestHandlerDelegate<TResponse> nextHandler,
        CancellationToken cancellationToken = default)
    {
        var response = await nextHandler();

        if (response is string str)
        {
            return (TResponse)(object)$"[SECOND]{str}[/SECOND]";
        }

        return response;
    }
}

public class ShortCircuitBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public ValueTask<TResponse> HandleAsync(
        TRequest request,
        RequestHandlerDelegate<TResponse> nextHandler,
        CancellationToken cancellationToken = default)
    {
        // Don't call nextHandler - short circuit the pipeline
        if (typeof(TResponse) == typeof(string))
        {
            return ValueTask.FromResult((TResponse)(object)"SHORT_CIRCUITED");
        }

        return default;
    }
}

public class ThrowingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public ValueTask<TResponse> HandleAsync(
        TRequest request,
        RequestHandlerDelegate<TResponse> nextHandler,
        CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("Behavior intentionally threw");
    }
}
