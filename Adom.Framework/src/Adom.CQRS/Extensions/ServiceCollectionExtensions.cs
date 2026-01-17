using System.Reflection;
using Adom.CQRS.Abstractions;
using Adom.CQRS.Configuration;
using Adom.CQRS.Discovery;
using Adom.CQRS.Dispatching;
using Microsoft.Extensions.DependencyInjection;

namespace Adom.CQRS.Extensions;

/// <summary>
/// Extension methods for configuring CQRS services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Adom.CQRS services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">Optional configuration action.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddAdomCqrs(
        this IServiceCollection services,
        Action<CqrsBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var builder = new CqrsBuilder(services);
        configure?.Invoke(builder);

        // Scan assemblies and register handlers
        var handlers = AssemblyScanner.ScanAndRegister(services, builder.AssembliesToScan);

        // Register handler registry as singleton
        var registry = new HandlerRegistry(handlers);
        services.AddSingleton(registry);

        // Register dispatcher as singleton
        services.AddSingleton<IDispatcher, Dispatcher>();

        return services;
    }

    /// <summary>
    /// Adds Adom.CQRS services with assembly scanning.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="assembliesToScan">The assemblies to scan for handlers.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddAdomCqrs(
        this IServiceCollection services,
        params Assembly[] assembliesToScan)
    {
        return AddAdomCqrs(services, builder => builder.ScanAssemblies(assembliesToScan));
    }
}
