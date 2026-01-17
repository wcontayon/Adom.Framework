# Feature Specification: CQRS Library

**Feature Branch**: `001-cqrs-library`
**Created**: 2026-01-17
**Status**: Clarified
**Project**: Adom.CQRS (NuGet package)
**Input**: User description: "A .Net library to use CQRS pattern. Library should be light, efficient, less memory usage and performant. We have the class Handler (generic) for handler, generic class IRequest (Command / IQuery). Each Handler can cache the result by using the interface ICacheableRequest on the IRequest. It will use the default IDistributedCache to cache it"

## Clarifications

### Session 2026-01-17

- Q: Should the library support pipeline behaviors for cross-cutting concerns? → A: Yes, include pipeline behaviors - handlers can be wrapped with pre/post logic
- Q: What is the minimum target framework for the NuGet package? → A: .NET 10 only - maximum performance, latest C# features
- Q: How should handlers be discovered? → A: Both - assembly scanning as default, source generators as optional performance mode
- Q: How should default cache keys be generated? → A: Type name + hash of JSON-serialized request properties (compact, reliable)
- Q: What should the primary dispatcher interface be named? → A: IDispatcher - explicit about routing/dispatching behavior

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Execute Command Request (Priority: P1)

As a developer, I want to create and execute command requests through handlers so that I can perform write operations in my application following the CQRS pattern.

**Why this priority**: Commands are fundamental to CQRS - they represent state-changing operations and are the core building block of the pattern. Without command handling, the library has no value.

**Independent Test**: Can be fully tested by creating a simple command (e.g., CreateUserCommand) with a handler that processes it, and verifying the handler executes successfully.

**Acceptance Scenarios**:

1. **Given** a command request implementing IRequest, **When** the request is dispatched to its handler, **Then** the handler processes the command and returns the expected result
2. **Given** a command handler, **When** registering it with the application, **Then** it is automatically discovered and available for dispatch
3. **Given** a command request, **When** the handler throws an exception, **Then** the exception propagates to the caller with full context

---

### User Story 2 - Execute Query Request (Priority: P1)

As a developer, I want to create and execute query requests through handlers so that I can perform read operations in my application following the CQRS pattern.

**Why this priority**: Queries are equally fundamental to CQRS - they represent read operations. Commands and queries together form the complete CQRS pattern.

**Independent Test**: Can be fully tested by creating a simple query (e.g., GetUserByIdQuery) with a handler that returns data, and verifying the correct result is returned.

**Acceptance Scenarios**:

1. **Given** a query request implementing IRequest, **When** the request is dispatched to its handler, **Then** the handler processes the query and returns the expected data
2. **Given** a query request with parameters, **When** dispatched, **Then** the handler receives all parameters correctly
3. **Given** a query that returns no results, **When** dispatched, **Then** the handler returns an appropriate empty response

---

### User Story 3 - Cache Query Results (Priority: P2)

As a developer, I want to cache query results automatically so that repeated identical queries are served from cache, improving performance and reducing load.

**Why this priority**: Caching is an optimization that builds on top of the core query functionality. It provides significant performance benefits but requires the base query mechanism to work first.

**Independent Test**: Can be fully tested by executing a cacheable query twice and verifying the second execution retrieves from cache (handler not invoked twice).

**Acceptance Scenarios**:

1. **Given** a query implementing ICacheableRequest, **When** executed for the first time, **Then** the result is stored in cache and returned
2. **Given** a cached query result exists, **When** the same query is executed again, **Then** the cached result is returned without invoking the handler
3. **Given** a cached result has expired, **When** the query is executed, **Then** the handler is invoked and the new result is cached
4. **Given** a query NOT implementing ICacheableRequest, **When** executed, **Then** no caching occurs and the handler is always invoked

---

### User Story 4 - Configure Cache Behavior (Priority: P3)

As a developer, I want to configure cache duration and cache key generation for cacheable requests so that I can control caching behavior per request type.

**Why this priority**: Configuration options enhance the caching feature but are not essential for basic caching functionality.

**Independent Test**: Can be fully tested by creating cacheable requests with different cache durations and verifying each respects its configured expiration.

**Acceptance Scenarios**:

1. **Given** a cacheable request with custom duration, **When** cached, **Then** the cache entry expires after the specified duration
2. **Given** a cacheable request with custom cache key logic, **When** cached, **Then** the custom key is used for cache storage and retrieval
3. **Given** two different cacheable requests with same parameters, **When** both are cached, **Then** each has its own distinct cache entry

---

### Edge Cases

- What happens when the cache service is unavailable? The library gracefully falls back to executing the handler directly without caching.
- What happens when a handler is not registered for a request? The library throws a descriptive exception indicating which handler is missing.
- What happens when a request is dispatched with null? The library throws an argument validation exception.
- How does the system handle concurrent requests for the same cacheable query? The first request populates the cache; subsequent requests wait or receive the cached result.
- What happens when cache serialization fails? The library logs the error and falls back to handler execution.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Library MUST provide a generic IRequest interface that serves as the base for both commands and queries
- **FR-002**: Library MUST provide a generic Handler base class/interface that processes IRequest instances and returns typed results
- **FR-003**: Library MUST support handlers that return no result (void/unit) for fire-and-forget commands
- **FR-004**: Library MUST provide an ICacheableRequest interface that requests can implement to opt into caching
- **FR-005**: Library MUST use the standard distributed cache abstraction for cache storage when ICacheableRequest is implemented
- **FR-006**: Library MUST allow cacheable requests to specify cache duration
- **FR-007**: Library MUST generate cache keys using request type name + hash of JSON-serialized properties by default
- **FR-007a**: Library MUST allow cacheable requests to override default cache key generation with custom logic
- **FR-008**: Library MUST provide a dispatcher/mediator to route requests to their appropriate handlers
- **FR-009**: Library MUST support automatic handler discovery via assembly scanning at startup (default mode)
- **FR-009a**: Library MUST provide optional source generator package for compile-time handler discovery (zero reflection)
- **FR-009b**: Library MUST allow explicit manual handler registration as an alternative to auto-discovery
- **FR-010**: Library MUST handle cache unavailability gracefully by falling back to direct handler execution
- **FR-011**: Library MUST provide clear exceptions when handlers are not found for requests
- **FR-012**: Library MUST support cancellation tokens for async operations
- **FR-013**: Library MUST minimize memory allocations during request dispatch and handling
- **FR-014**: Library MUST support dependency injection for handlers
- **FR-015**: Library MUST support pipeline behaviors that wrap handler execution with pre/post processing logic
- **FR-016**: Library MUST allow multiple pipeline behaviors to be chained in a configurable order
- **FR-017**: Library MUST implement caching as a built-in pipeline behavior (using ICacheableRequest)

### Key Entities

- **IRequest<TResponse>**: Represents a request (command or query) that expects a response of type TResponse
- **IRequest**: Represents a request with no return value (commands that don't return data)
- **IHandler<TRequest, TResponse>**: Processes a specific request type and produces a response
- **ICacheableRequest**: Marker interface with cache configuration that enables caching for a request
- **IDispatcher**: Routes requests to their corresponding handlers and manages the request pipeline
- **IPipelineBehavior<TRequest, TResponse>**: Wraps handler execution to add cross-cutting concerns (logging, validation, caching, etc.)

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Developers can create and dispatch a command/query request in under 5 lines of code
- **SC-002**: Request dispatch overhead adds less than 1 millisecond latency compared to direct handler invocation
- **SC-003**: Cached query responses are returned within 1 millisecond when cache is available
- **SC-004**: Library memory footprint for dispatch operations produces zero allocations on hot paths
- **SC-005**: 100% of handlers are automatically discovered without manual registration code
- **SC-006**: Cache hit rate for cacheable requests reaches expected levels based on configured TTL and access patterns
- **SC-007**: Library integrates with application dependency injection with a single registration call

## Assumptions

- The host application uses dependency injection (standard pattern for modern applications)
- The distributed cache is already configured in the host application when caching features are used
- Handlers are stateless and thread-safe
- Request objects are immutable after creation
- Cache serialization uses the default JSON serialization unless the host configures otherwise
- The library targets high-performance scenarios and prioritizes zero-allocation patterns

## Technical Constraints

- **Target Framework**: .NET 10 only (net10.0)
- **C# Version**: Latest (C# 13+)
- **Package ID**: Adom.CQRS (core library)
- **Package ID**: Adom.CQRS.SourceGenerators (optional compile-time discovery)
- **Performance**: Leverages .NET 10 features including Span<T>, SearchValues, native AOT compatibility
- **Discovery Modes**: Assembly scanning (default, runtime) or source generators (optional, compile-time)
- **No backward compatibility**: Does not support .NET 9 or earlier versions

## Out of Scope

- Providing a distributed cache implementation (uses host-provided cache)
- Request validation (can be added via pipeline behaviors in future versions)
- Request/response logging (can be added via pipeline behaviors in future versions)
- Retry policies and circuit breakers (handled at infrastructure level)
- Event sourcing integration
