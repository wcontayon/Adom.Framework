using System.Reflection;
using Adom.CQRS.Abstractions;
using Adom.CQRS.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Adom.CQRS.Tests;

public class HandlerDiscoveryTests
{
    [Fact]
    public void AddAdomCqrs_ShouldDiscoverHandlersInAssembly()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<HandlerDiscoveryTests>());
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var dispatcher = serviceProvider.GetService<IDispatcher>();
        dispatcher.Should().NotBeNull();
    }

    [Fact]
    public async Task AddAdomCqrs_WithMultipleAssemblies_ShouldDiscoverAllHandlers()
    {
        // Arrange
        var services = new ServiceCollection();
        var assembly1 = typeof(HandlerDiscoveryTests).Assembly;
        var assembly2 = typeof(IDispatcher).Assembly;

        // Act
        services.AddAdomCqrs(builder => builder.ScanAssemblies(assembly1, assembly2));
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var command = new DiscoveryTestCommand("test");

        // Assert
        var result = await dispatcher.DispatchAsync<DiscoveryTestCommand, string>(command);
        result.Should().Be("discovered: test");
    }

    [Fact]
    public void AddAdomCqrs_ShouldRegisterDispatcherAsSingleton()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<HandlerDiscoveryTests>());
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var dispatcher1 = serviceProvider.GetRequiredService<IDispatcher>();
        var dispatcher2 = serviceProvider.GetRequiredService<IDispatcher>();

        // Assert
        dispatcher1.Should().BeSameAs(dispatcher2);
    }

    [Fact]
    public void AddAdomCqrs_ShouldRegisterHandlersAsTransient()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<HandlerDiscoveryTests>());
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var handler1 = serviceProvider.GetRequiredService<IHandler<DiscoveryTestCommand, string>>();
        var handler2 = serviceProvider.GetRequiredService<IHandler<DiscoveryTestCommand, string>>();

        // Assert
        handler1.Should().NotBeSameAs(handler2);
    }

    [Fact]
    public void AddAdomCqrs_WithNoAssemblies_ShouldNotThrow()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        Action act = () => services.AddAdomCqrs();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ScanAssemblyContaining_ShouldAddCorrectAssembly()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<HandlerDiscoveryTests>());
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var dispatcher = serviceProvider.GetService<IDispatcher>();
        dispatcher.Should().NotBeNull();
    }

}

// Test commands and handlers for discovery
public record DiscoveryTestCommand(string Value) : IRequest<string>;

public class DiscoveryTestCommandHandler : IHandler<DiscoveryTestCommand, string>
{
    public ValueTask<string> HandleAsync(DiscoveryTestCommand request, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult($"discovered: {request.Value}");
    }
}
