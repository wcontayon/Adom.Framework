using System.Reflection;
using Adom.CQRS.Abstractions;
using Adom.CQRS.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Adom.CQRS.Discovery;

/// <summary>
/// Scans assemblies for handler implementations at startup.
/// </summary>
internal static class AssemblyScanner
{
    /// <summary>
    /// Scans the specified assemblies for IHandler implementations and registers them.
    /// </summary>
    /// <param name="services">The service collection to register handlers in.</param>
    /// <param name="assemblies">The assemblies to scan.</param>
    /// <returns>A dictionary of request types to handler descriptors.</returns>
    public static Dictionary<Type, HandlerDescriptor> ScanAndRegister(
        IServiceCollection services,
        IEnumerable<Assembly> assemblies)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(assemblies);

        var handlers = new Dictionary<Type, HandlerDescriptor>();

        foreach (var assembly in assemblies)
        {
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && ImplementsIHandler(t));

            foreach (var handlerType in handlerTypes)
            {
                var handlerInterfaces = handlerType.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<,>));

                foreach (var handlerInterface in handlerInterfaces)
                {
                    var genericArgs = handlerInterface.GetGenericArguments();
                    var requestType = genericArgs[0];
                    var responseType = genericArgs[1];

                    // Register handler as transient
                    services.AddTransient(handlerInterface, handlerType);

                    // Create factory delegate
                    var factory = CreateFactory(handlerInterface);

                    // Store descriptor
                    handlers[requestType] = new HandlerDescriptor
                    {
                        HandlerType = handlerType,
                        RequestType = requestType,
                        ResponseType = responseType,
                        Factory = factory
                    };
                }
            }
        }

        return handlers;
    }

    private static bool ImplementsIHandler(Type type)
    {
        return type.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<,>));
    }

    private static Func<IServiceProvider, object> CreateFactory(Type handlerInterfaceType)
    {
        return serviceProvider =>
        {
            var handler = serviceProvider.GetRequiredService(handlerInterfaceType);
            return handler ?? throw new InvalidOperationException($"Failed to resolve handler of type '{handlerInterfaceType.FullName}'.");
        };
    }
}
