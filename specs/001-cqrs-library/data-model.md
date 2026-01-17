# Data Model: CQRS Library (Adom.CQRS)

**Date**: 2026-01-17
**Feature**: 001-cqrs-library

## Core Abstractions

### IRequest<TResponse>

Represents a request (command or query) that expects a response.

| Property | Type | Description |
|----------|------|-------------|
| (marker interface) | - | No properties; serves as type constraint |

**Constraints**:
- TResponse can be any type including value types
- Request objects should be immutable after creation
- Request objects must be serializable for caching

---

### IRequest

Represents a request with no return value (void commands).

| Property | Type | Description |
|----------|------|-------------|
| (marker interface) | - | Inherits from `IRequest<Unit>` |

**Note**: `Unit` is a zero-size struct representing void in a type-safe way.

---

### IHandler<TRequest, TResponse>

Processes a specific request type and produces a response.

| Method | Signature | Description |
|--------|-----------|-------------|
| HandleAsync | `ValueTask<TResponse> HandleAsync(TRequest request, CancellationToken ct)` | Processes the request and returns a response |

**Constraints**:
- `TRequest` must implement `IRequest<TResponse>`
- Handlers are stateless and thread-safe
- Handlers are registered as transient in DI

---

### IPipelineBehavior<TRequest, TResponse>

Wraps handler execution to add cross-cutting concerns.

| Method | Signature | Description |
|--------|-----------|-------------|
| HandleAsync | `ValueTask<TResponse> HandleAsync(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)` | Wraps the next behavior/handler in the pipeline |

**Delegate**:
```csharp
public delegate ValueTask<TResponse> RequestHandlerDelegate<TResponse>();
```

**Constraints**:
- Pipeline behaviors execute in registration order
- Each behavior can short-circuit by not calling `next()`
- Registered as scoped in DI

---

### ICacheableRequest

Marker interface that enables caching for a request.

| Property | Type | Description |
|----------|------|-------------|
| CacheDuration | `TimeSpan` | How long to cache the response |
| CacheKey | `string?` | Optional custom cache key (null = use default generator) |

**Default Behavior**:
- If `CacheKey` is null, key is generated as: `{TypeName}:{XxHash128(JsonSerialize(this))}`
- Cache uses `IDistributedCache` from DI container

---

### IDispatcher

Routes requests to their corresponding handlers through the pipeline.

| Method | Signature | Description |
|--------|-----------|-------------|
| DispatchAsync<TResponse> | `ValueTask<TResponse> DispatchAsync<TRequest, TResponse>(TRequest request, CancellationToken ct)` | Dispatches a request that returns a response |
| DispatchAsync | `ValueTask DispatchAsync<TRequest>(TRequest request, CancellationToken ct)` | Dispatches a void request |

**Constraints**:
- Registered as singleton in DI
- Thread-safe after initialization
- Throws `HandlerNotFoundException` if no handler registered

---

## Internal Types

### HandlerDescriptor

Internal value type storing handler metadata.

| Field | Type | Description |
|-------|------|-------------|
| HandlerType | `Type` | The concrete handler type |
| RequestType | `Type` | The request type this handler processes |
| ResponseType | `Type` | The response type the handler returns |
| Factory | `Func<IServiceProvider, object>` | Compiled factory delegate |

---

### CacheEntry

Internal struct for cached responses.

| Field | Type | Description |
|-------|------|-------------|
| Value | `byte[]` | Serialized response |
| ExpiresAt | `DateTimeOffset` | When the entry expires |

---

### Unit

Zero-size struct representing void.

| Property | Type | Description |
|----------|------|-------------|
| Value | `Unit` | Static singleton instance |

**Usage**: Return type for commands that don't return data.

---

## Exceptions

### HandlerNotFoundException

Thrown when no handler is registered for a request type.

| Property | Type | Description |
|----------|------|-------------|
| RequestType | `Type` | The request type that has no handler |
| Message | `string` | Descriptive error message |

---

### DispatcherException

General dispatcher error wrapper.

| Property | Type | Description |
|----------|------|-------------|
| RequestType | `Type` | The request type being dispatched |
| InnerException | `Exception` | The underlying exception |

---

## Configuration Types

### CqrsOptions

Configuration options for the CQRS library.

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| EnableCaching | `bool` | `true` | Whether to enable the caching pipeline behavior |
| DefaultCacheDuration | `TimeSpan` | 5 minutes | Default cache duration if not specified on request |
| ThrowOnMissingHandler | `bool` | `true` | Whether to throw or return default when handler not found |

---

### CqrsBuilder

Fluent builder for configuring CQRS services.

| Method | Description |
|--------|-------------|
| ScanAssemblies(params Assembly[]) | Add assemblies to scan for handlers |
| ScanAssemblyContaining<T>() | Add assembly containing type T |
| AddBehavior<T>() | Add a pipeline behavior |
| ConfigureCache(Action<CacheOptions>) | Configure caching options |

---

## Entity Relationships

```
IRequest<TResponse>
    └── Implemented by: Command/Query classes
    └── Optionally implements: ICacheableRequest

IHandler<TRequest, TResponse>
    └── Processes: IRequest<TResponse>
    └── Registered in: HandlerDescriptor

IPipelineBehavior<TRequest, TResponse>
    └── Wraps: IHandler execution
    └── Chain order: Registration order

IDispatcher
    └── Uses: HandlerRegistry (FrozenDictionary)
    └── Executes: Pipeline -> Handler
    └── Integrates: IDistributedCache (optional)
```

## State Transitions

### Request Dispatch Flow

```
1. [Created] Request object instantiated
2. [Dispatching] IDispatcher.DispatchAsync called
3. [Pipeline] Pipeline behaviors execute in order
   3a. [Cached] If ICacheableRequest and cache hit → return cached
   3b. [Executing] Handler.HandleAsync invoked
4. [Caching] If ICacheableRequest → store in cache
5. [Completed] Response returned to caller
```

### Handler Registration Flow

```
1. [Startup] Application calls AddAdomCqrs()
2. [Scanning] Assembly scanner finds IHandler implementations
3. [Registering] Handlers added to DI as transient
4. [Freezing] HandlerRegistry frozen to FrozenDictionary
5. [Ready] Dispatcher ready to process requests
```
