using System.Reflection;
using Adom.CQRS.Abstractions;
using Adom.CQRS.Caching;
using Microsoft.Extensions.DependencyInjection;

namespace Adom.CQRS.Configuration;

/// <summary>
/// Fluent builder for configuring CQRS services.
/// </summary>
/// <param name="services">The service collection.</param>
public sealed class CqrsBuilder(IServiceCollection services)
{
    private readonly IServiceCollection _services = services ?? throw new ArgumentNullException(nameof(services));
    private readonly List<Assembly> _assembliesToScan = [];

    /// <summary>
    /// Gets the service collection being configured.
    /// </summary>
    internal IServiceCollection Services => _services;

    /// <summary>
    /// Gets the assemblies to scan for handlers.
    /// </summary>
    internal IEnumerable<Assembly> AssembliesToScan => _assembliesToScan;

    /// <summary>
    /// Scans assemblies for handlers.
    /// </summary>
    /// <param name="assemblies">The assemblies to scan.</param>
    /// <returns>The builder for method chaining.</returns>
    public CqrsBuilder ScanAssemblies(params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(assemblies);
        _assembliesToScan.AddRange(assemblies);
        return this;
    }

    /// <summary>
    /// Scans the assembly containing the specified type.
    /// </summary>
    /// <typeparam name="T">A type in the assembly to scan.</typeparam>
    /// <returns>The builder for method chaining.</returns>
    public CqrsBuilder ScanAssemblyContaining<T>()
    {
        _assembliesToScan.Add(typeof(T).Assembly);
        return this;
    }

    /// <summary>
    /// Adds a pipeline behavior.
    /// </summary>
    /// <typeparam name="TBehavior">The behavior type (open generic).</typeparam>
    /// <returns>The builder for method chaining.</returns>
#pragma warning disable CA2263 // Open generic registration requires non-generic overload
    public CqrsBuilder AddBehavior<TBehavior>()
        where TBehavior : class
    {
        _services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TBehavior));
        return this;
    }
#pragma warning restore CA2263

    /// <summary>
    /// Adds a pipeline behavior with specific request/response types.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <typeparam name="TBehavior">The behavior type.</typeparam>
    /// <returns>The builder for method chaining.</returns>
    public CqrsBuilder AddBehavior<TRequest, TResponse, TBehavior>()
        where TRequest : IRequest<TResponse>
        where TBehavior : class, IPipelineBehavior<TRequest, TResponse>
    {
        _services.AddScoped<IPipelineBehavior<TRequest, TResponse>, TBehavior>();
        return this;
    }

    /// <summary>
    /// Configures automatic caching for requests implementing <see cref="ICacheableRequest"/>.
    /// Requires <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/> to be registered in DI.
    /// </summary>
    /// <param name="configure">Optional action to configure cache options.</param>
    /// <returns>The builder for method chaining.</returns>
    public CqrsBuilder ConfigureCaching(Action<CacheOptions>? configure = null)
    {
        // Register cache options
        if (configure is not null)
        {
            _services.Configure(configure);
        }
        else
        {
            _services.Configure<CacheOptions>(_ => { });
        }

        // Register caching behavior (open generic)
        _services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

        return this;
    }
}
