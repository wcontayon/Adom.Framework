using Adom.CQRS;
using Adom.CQRS.Abstractions;
using Adom.CQRS.Exceptions;
using Adom.CQRS.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Adom.CQRS.Tests;

public class DispatcherTests
{
    [Fact]
    public async Task DispatchAsync_WithValidCommand_ShouldInvokeHandler()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<DispatcherTests>());
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var command = new TestCommand("test-data");

        // Act
        var result = await dispatcher.DispatchAsync<TestCommand, string>(command);

        // Assert
        result.Should().Be("HANDLED: test-data");
    }

    [Fact]
    public async Task DispatchAsync_WithMultipleCommands_ShouldInvokeCorrectHandlers()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<DispatcherTests>());
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var command1 = new TestCommand("first");
        var command2 = new AnotherTestCommand(42);

        // Act
        var result1 = await dispatcher.DispatchAsync<TestCommand, string>(command1);
        var result2 = await dispatcher.DispatchAsync<AnotherTestCommand, int>(command2);

        // Assert
        result1.Should().Be("HANDLED: first");
        result2.Should().Be(84); // 42 * 2
    }

    [Fact]
    public async Task DispatchAsync_WithNullRequest_ShouldThrowArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<DispatcherTests>());
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        TestCommand? command = null;

        // Act
        Func<Task> act = async () => await dispatcher.DispatchAsync<TestCommand, string>(command!);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("request");
    }

    [Fact]
    public async Task DispatchAsync_WithUnregisteredCommand_ShouldThrowHandlerNotFoundException()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<DispatcherTests>());
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var command = new UnregisteredCommand();

        // Act
        Func<Task> act = async () => await dispatcher.DispatchAsync<UnregisteredCommand, string>(command);

        // Assert
        await act.Should().ThrowAsync<HandlerNotFoundException>()
            .Where(ex => ex.RequestType == typeof(UnregisteredCommand));
    }

    [Fact]
    public async Task DispatchAsync_WhenHandlerThrowsException_ShouldPropagateException()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<DispatcherTests>());
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var command = new ThrowingCommand();

        // Act
        Func<Task> act = async () => await dispatcher.DispatchAsync<ThrowingCommand, string>(command);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Handler intentionally threw exception");
    }

    [Fact]
    public async Task DispatchAsync_WithCancellationToken_ShouldPassTokenToHandler()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<DispatcherTests>());
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var cts = new CancellationTokenSource();
        cts.Cancel();
        var command = new CancellableCommand();

        // Act
        Func<Task> act = async () => await dispatcher.DispatchAsync<CancellableCommand, string>(command, cts.Token);

        // Assert
        await act.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task DispatchAsync_WithQueryAndParameters_ShouldReturnCorrectResult()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<DispatcherTests>());
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var query = new GetUserByIdQuery(Guid.NewGuid(), "TestUser");

        // Act
        var result = await dispatcher.DispatchAsync<GetUserByIdQuery, UserDto>(query);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(query.UserId);
        result.Name.Should().Be("TestUser");
        result.Email.Should().Be("testuser@example.com");
    }

    [Fact]
    public async Task DispatchAsync_WithVoidCommand_ShouldExecuteSuccessfully()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<DispatcherTests>());
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var command = new VoidCommand("test-data");

        // Act
        Func<Task> act = async () => await dispatcher.DispatchAsync(command);

        // Assert
        await act.Should().NotThrowAsync();
    }
}

// Test commands and handlers
public record TestCommand(string Data) : IRequest<string>;

public class TestCommandHandler : IHandler<TestCommand, string>
{
    public ValueTask<string> HandleAsync(TestCommand request, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult($"HANDLED: {request.Data}");
    }
}

public record AnotherTestCommand(int Value) : IRequest<int>;

public class AnotherTestCommandHandler : IHandler<AnotherTestCommand, int>
{
    public ValueTask<int> HandleAsync(AnotherTestCommand request, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(request.Value * 2);
    }
}

public record UnregisteredCommand : IRequest<string>;

public record ThrowingCommand : IRequest<string>;

public class ThrowingCommandHandler : IHandler<ThrowingCommand, string>
{
    public ValueTask<string> HandleAsync(ThrowingCommand request, CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("Handler intentionally threw exception");
    }
}

public record CancellableCommand : IRequest<string>;

public class CancellableCommandHandler : IHandler<CancellableCommand, string>
{
    public ValueTask<string> HandleAsync(CancellableCommand request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return ValueTask.FromResult("Completed");
    }
}

// User Story 2 test types
public record GetUserByIdQuery(Guid UserId, string Name) : IRequest<UserDto>;

public record UserDto(Guid Id, string Name, string Email);

public class GetUserByIdQueryHandler : IHandler<GetUserByIdQuery, UserDto>
{
    public ValueTask<UserDto> HandleAsync(GetUserByIdQuery request, CancellationToken cancellationToken = default)
    {
        var email = $"{request.Name.ToLowerInvariant()}@example.com";
        return ValueTask.FromResult(new UserDto(request.UserId, request.Name, email));
    }
}

public record VoidCommand(string Data) : IRequest;

public class VoidCommandHandler : IHandler<VoidCommand>
{
    public ValueTask<Unit> HandleAsync(VoidCommand request, CancellationToken cancellationToken = default)
    {
        // Command executed successfully
        return ValueTask.FromResult(Unit.Value);
    }
}
