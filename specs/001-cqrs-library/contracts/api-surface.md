# API Surface Contract: Adom.CQRS

**Version**: 1.0.0
**Date**: 2026-01-17
**Target**: .NET 10 (net10.0)

## Public API Summary

This document defines the public API surface of the Adom.CQRS library. All types listed here are part of the public contract and follow semantic versioning.

---

## Namespace: Adom.CQRS

### Interfaces

#### IRequest<TResponse>

```csharp
/// <summary>
/// Marker interface for requests that return a response.
/// </summary>
/// <typeparam name="TResponse">The type of response this request produces.</typeparam>
public interface IRequest<out TResponse> { }
```

#### IRequest

```csharp
/// <summary>
/// Marker interface for requests that return no value.
/// </summary>
public interface IRequest : IRequest<Unit> { }
```

#### IHandler<TRequest, TResponse>

```csharp
/// <summary>
/// Handles a specific request type and produces a response.
/// </summary>
public interface IHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handles the request asynchronously.
    /// </summary>
    ValueTask<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}
```

#### IHandler<TRequest>

```csharp
/// <summary>
/// Handles a request that returns no value.
/// </summary>
public interface IHandler<in TRequest> : IHandler<TRequest, Unit>
    where TRequest : IRequest { }
```

#### IDispatcher

```csharp
/// <summary>
/// Dispatches requests to their corresponding handlers.
/// </summary>
public interface IDispatcher
{
    /// <summary>
    /// Dispatches a request and returns the response.
    /// </summary>
    ValueTask<TResponse> DispatchAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>;

    /// <summary>
    /// Dispatches a request that returns no value.
    /// </summary>
    ValueTask DispatchAsync<TRequest>(
        TRequest request,
        CancellationToken cancellationToken = default)
        where TRequest : IRequest;
}
```

#### IPipelineBehavior<TRequest, TResponse>

```csharp
/// <summary>
/// Pipeline behavior that wraps handler execution.
/// </summary>
public interface IPipelineBehavior<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handles the request, optionally delegating to the next behavior.
    /// </summary>
    ValueTask<TResponse> HandleAsync(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default);
}
```

#### ICacheableRequest

```csharp
/// <summary>
/// Marker interface that enables response caching for a request.
/// </summary>
public interface ICacheableRequest
{
    /// <summary>
    /// Duration to cache the response.
    /// </summary>
    TimeSpan CacheDuration { get; }

    /// <summary>
    /// Optional custom cache key. If null, default key generation is used.
    /// </summary>
    string? CacheKey => null;
}
```

---

### Delegates

```csharp
/// <summary>
/// Represents the next handler or behavior in the pipeline.
/// </summary>
public delegate ValueTask<TResponse> RequestHandlerDelegate<TResponse>();
```

---

### Structs

#### Unit

```csharp
/// <summary>
/// Represents a void return type in a type-safe way.
/// </summary>
public readonly struct Unit : IEquatable<Unit>
{
    /// <summary>
    /// The singleton Unit value.
    /// </summary>
    public static readonly Unit Value = default;

    public bool Equals(Unit other) => true;
    public override bool Equals(object? obj) => obj is Unit;
    public override int GetHashCode() => 0;
    public override string ToString() => "()";

    public static bool operator ==(Unit left, Unit right) => true;
    public static bool operator !=(Unit left, Unit right) => false;
}
```

---

### Exceptions

#### HandlerNotFoundException

```csharp
/// <summary>
/// Thrown when no handler is registered for a request type.
/// </summary>
public sealed class HandlerNotFoundException : Exception
{
    public Type RequestType { get; }

    public HandlerNotFoundException(Type requestType)
        : base($"No handler registered for request type '{requestType.FullName}'.")
    {
        RequestType = requestType;
    }
}
```

#### DispatcherException

```csharp
/// <summary>
/// Thrown when an error occurs during request dispatch.
/// </summary>
public sealed class DispatcherException : Exception
{
    public Type RequestType { get; }

    public DispatcherException(Type requestType, Exception innerException)
        : base($"Error dispatching request of type '{requestType.FullName}'.", innerException)
    {
        RequestType = requestType;
    }
}
```

---

## Namespace: Adom.CQRS.Extensions

### Extension Methods

#### ServiceCollectionExtensions

```csharp
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Adom.CQRS services to the service collection.
    /// </summary>
    public static IServiceCollection AddAdomCqrs(
        this IServiceCollection services,
        Action<CqrsBuilder>? configure = null);

    /// <summary>
    /// Adds Adom.CQRS services with assembly scanning.
    /// </summary>
    public static IServiceCollection AddAdomCqrs(
        this IServiceCollection services,
        params Assembly[] assembliesToScan);
}
```

---

## Namespace: Adom.CQRS.Configuration

### Classes

#### CqrsBuilder

```csharp
/// <summary>
/// Fluent builder for configuring CQRS services.
/// </summary>
public sealed class CqrsBuilder
{
    /// <summary>
    /// Scans assemblies for handlers.
    /// </summary>
    public CqrsBuilder ScanAssemblies(params Assembly[] assemblies);

    /// <summary>
    /// Scans the assembly containing the specified type.
    /// </summary>
    public CqrsBuilder ScanAssemblyContaining<T>();

    /// <summary>
    /// Adds a pipeline behavior.
    /// </summary>
    public CqrsBuilder AddBehavior<TBehavior>()
        where TBehavior : class;

    /// <summary>
    /// Adds a pipeline behavior with specific request/response types.
    /// </summary>
    public CqrsBuilder AddBehavior<TRequest, TResponse, TBehavior>()
        where TRequest : IRequest<TResponse>
        where TBehavior : class, IPipelineBehavior<TRequest, TResponse>;

    /// <summary>
    /// Configures caching options.
    /// </summary>
    public CqrsBuilder ConfigureCaching(Action<CacheOptions> configure);

    /// <summary>
    /// Disables the built-in caching behavior.
    /// </summary>
    public CqrsBuilder DisableCaching();
}
```

#### CacheOptions

```csharp
/// <summary>
/// Configuration options for request caching.
/// </summary>
public sealed class CacheOptions
{
    /// <summary>
    /// Default cache duration for requests that don't specify one.
    /// </summary>
    public TimeSpan DefaultDuration { get; set; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Whether to throw or silently continue when cache is unavailable.
    /// </summary>
    public bool ThrowOnCacheFailure { get; set; } = false;

    /// <summary>
    /// Custom cache key prefix.
    /// </summary>
    public string KeyPrefix { get; set; } = "adom:cqrs:";
}
```

---

## Breaking Change Policy

Changes to the public API surface follow semantic versioning:

- **MAJOR**: Removing types/members, changing signatures, breaking behavioral changes
- **MINOR**: Adding new types/members, additive behavioral changes
- **PATCH**: Bug fixes, performance improvements, documentation

Types in `Adom.CQRS.Internal` namespace are not part of the public API and may change without notice.
