# Research: CQRS Library (Adom.CQRS)

**Date**: 2026-01-17
**Feature**: 001-cqrs-library
**Status**: Complete

## Research Tasks

### 1. Zero-Allocation Dispatcher Pattern

**Decision**: Use `FrozenDictionary<Type, HandlerDescriptor>` for handler lookup with compiled delegate invocation.

**Rationale**:
- `FrozenDictionary<T>` provides O(1) lookup with zero allocations after initialization
- Compiled delegates avoid reflection overhead during dispatch
- Handler descriptors stored as value types where possible

**Alternatives Considered**:
- `ConcurrentDictionary<Type, Func<...>>`: Higher allocation overhead, unnecessary concurrency for read-only registry
- Direct reflection: Too slow, causes allocations on every dispatch
- Expression tree compilation: Good performance but more complex than delegates

### 2. Pipeline Behavior Chain Implementation

**Decision**: Use a recursive delegate chain with `ValueTask<TResponse>` return type.

**Rationale**:
- `ValueTask<T>` avoids allocation when operations complete synchronously (common case)
- Recursive chain allows behaviors to wrap inner execution naturally
- No intermediate collections needed for pipeline traversal

**Alternatives Considered**:
- `IAsyncEnumerable` pipeline: Unnecessary complexity, allocation overhead
- Middleware-style next delegate: Similar but less type-safe
- Decorator pattern: Requires more boilerplate for users

### 3. Cache Key Generation Strategy

**Decision**: Use `XxHash128` for hashing JSON-serialized request properties with type name prefix.

**Rationale**:
- XxHash128 is extremely fast and available in .NET
- 128-bit hash provides sufficient collision resistance
- JSON serialization handles nested objects correctly
- Type name prefix ensures different request types never collide

**Pattern**:
```
CacheKey = $"{RequestTypeName}:{XxHash128(JsonSerialize(request))}"
```

**Alternatives Considered**:
- SHA256: Cryptographically secure but slower than needed for cache keys
- GetHashCode(): Not stable across processes, 32-bit collision risk
- Manual property concatenation: Error-prone, doesn't handle complex objects

### 4. Handler Discovery via Assembly Scanning

**Decision**: Use `AssemblyLoadContext` reflection with `FrozenDictionary` caching at startup.

**Rationale**:
- Reflection only happens once at application startup
- Results cached in frozen dictionary for zero-allocation lookup
- Supports multiple assemblies via configuration

**Registration Pattern**:
```csharp
services.AddAdomCqrs(config => config
    .ScanAssemblies(typeof(MyHandler).Assembly)
    .ScanAssemblyContaining<MyHandler>());
```

**Alternatives Considered**:
- Convention-based discovery: Less explicit, harder to debug
- Attribute markers: Extra boilerplate for users

### 5. Source Generator Implementation

**Decision**: Generate a static `Register` method that directly registers all handlers without reflection.

**Rationale**:
- Compile-time discovery eliminates all runtime reflection
- Native AOT compatible (no `Type.GetType()` or assembly scanning)
- Generated code is inspectable and debuggable

**Generated Pattern**:
```csharp
[GeneratedCode("Adom.CQRS.SourceGenerators", "1.0.0")]
public static class AdomCqrsRegistration
{
    public static IServiceCollection AddGeneratedHandlers(this IServiceCollection services)
    {
        services.AddTransient<IHandler<CreateUserCommand, UserId>, CreateUserCommandHandler>();
        // ... all discovered handlers
        return services;
    }
}
```

**Alternatives Considered**:
- IL weaving: Complex, harder to debug
- T4 templates: Not part of build, manual step required

### 6. IDistributedCache Integration

**Decision**: Implement caching as a pipeline behavior that intercepts cacheable requests.

**Rationale**:
- Clean separation of concerns (caching is orthogonal to handling)
- Easy to enable/disable per request type
- Follows existing Adom.Framework patterns

**Behavior Order**: Caching behavior should execute early in the pipeline (before validation/logging) to return cached results as fast as possible.

**Alternatives Considered**:
- Handler decorator: More invasive, requires wrapper for each handler
- Dispatcher-level caching: Less flexible, harder to configure per-request

### 7. Cancellation Token Propagation

**Decision**: All async methods accept `CancellationToken` as the last parameter, propagated through the entire pipeline.

**Rationale**:
- Standard .NET async pattern
- Allows graceful cancellation at any point
- Required for proper ASP.NET Core integration

**Pattern**:
```csharp
ValueTask<TResponse> DispatchAsync<TRequest, TResponse>(
    TRequest request,
    CancellationToken cancellationToken = default);
```

### 8. Exception Handling Strategy

**Decision**: Use centralized `ThrowHelper` class with non-inlined throw methods.

**Rationale**:
- Follows Adom.Framework constitution pattern
- Keeps hot paths small for JIT inlining
- Consistent exception messages across library

**Exceptions Defined**:
- `HandlerNotFoundException`: No handler registered for request type
- `DispatcherException`: General dispatcher errors (wraps inner exceptions)

### 9. DI Lifetime Strategy

**Decision**: Handlers registered as `Transient`, pipeline behaviors as `Scoped`, dispatcher as `Singleton`.

**Rationale**:
- Handlers are typically lightweight and stateless (transient)
- Pipeline behaviors may need request-scoped state (scoped)
- Dispatcher is thread-safe and immutable after initialization (singleton)

**Alternatives Considered**:
- All singletons: Prevents handlers from using scoped dependencies
- All scoped: Unnecessary overhead for stateless handlers

### 10. Native AOT Compatibility

**Decision**: Mark all public types with `[DynamicallyAccessedMembers]` attributes and use source generators for full AOT support.

**Rationale**:
- .NET 10 has improved AOT support
- Source generator mode provides true zero-reflection operation
- Assembly scanning mode works with proper trimmer annotations

**AOT-Safe Pattern**:
```csharp
public interface IHandler<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] TRequest, TResponse>
    where TRequest : IRequest<TResponse>
```

## Dependencies Research

### Microsoft.Extensions.DependencyInjection.Abstractions

**Version**: Latest stable for .NET 10
**Purpose**: DI integration via `IServiceCollection` extensions
**Impact**: Required dependency, small footprint

### Microsoft.Extensions.Caching.Abstractions

**Version**: Latest stable for .NET 10
**Purpose**: `IDistributedCache` interface for caching behavior
**Impact**: Optional at runtime (null-check before caching)

### System.Text.Json

**Version**: Built into .NET 10
**Purpose**: Cache key serialization
**Impact**: No additional dependency, uses framework-provided

### System.IO.Hashing

**Version**: Built into .NET 10
**Purpose**: XxHash128 for cache key hashing
**Impact**: No additional dependency, uses framework-provided

## Resolved Clarifications

All technical unknowns from the spec have been resolved through this research:

| Unknown | Resolution |
|---------|------------|
| Handler lookup strategy | FrozenDictionary with compiled delegates |
| Pipeline implementation | Recursive ValueTask delegate chain |
| Cache key algorithm | XxHash128 of JSON-serialized request |
| Source generator output | Static Register method with direct service registration |
| AOT compatibility | Trimmer annotations + source generator mode |

## Next Steps

Proceed to Phase 1: Design & Contracts
- Generate `data-model.md` with entity definitions
- Create API contracts in `/contracts/`
- Write `quickstart.md` with usage examples
