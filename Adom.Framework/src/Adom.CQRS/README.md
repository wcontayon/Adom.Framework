# Adom.CQRS

High-performance CQRS library for .NET 10+ providing request/handler dispatching with optional caching via IDistributedCache.

## Features

- **Zero-allocation patterns** on hot paths with FrozenDictionary and aggressive inlining
- **Pipeline behaviors** for cross-cutting concerns (logging, validation, caching)
- **Automatic caching** with ICacheableRequest interface and IDistributedCache
- **Dual handler discovery** modes: runtime assembly scanning + source generators (AOT-ready)
- **Full async support** with ValueTask<T> to avoid allocations
- **Type-safe** dispatching with full nullable reference type support
- **Minimal dependencies** - only Microsoft.Extensions abstractions

## Installation

```bash
dotnet add package Adom.CQRS
```

## Quick Start

### 1. Define a Query

```csharp
public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;

public record UserDto(Guid Id, string Name, string Email);
```

### 2. Implement a Handler

```csharp
public class GetUserByIdQueryHandler : IHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _repository;

    public GetUserByIdQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<UserDto> HandleAsync(
        GetUserByIdQuery request,
        CancellationToken cancellationToken = default)
    {
        var user = await _repository.GetByIdAsync(request.UserId, cancellationToken);
        return new UserDto(user.Id, user.Name, user.Email);
    }
}
```

### 3. Register Services

```csharp
// In Program.cs or Startup.cs
builder.Services.AddAdomCqrs(config => config
    .ScanAssemblyContaining<GetUserByIdQueryHandler>());
```

### 4. Dispatch Requests

```csharp
public class UsersController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public UsersController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid id, CancellationToken ct)
    {
        var user = await _dispatcher.DispatchAsync<GetUserByIdQuery, UserDto>(
            new GetUserByIdQuery(id), ct);
        return Ok(user);
    }
}
```

## Commands (Void Requests)

For operations that don't return a value:

```csharp
public record CreateUserCommand(string Name, string Email) : IRequest;

public class CreateUserCommandHandler : IHandler<CreateUserCommand>
{
    public async ValueTask<Unit> HandleAsync(
        CreateUserCommand request,
        CancellationToken cancellationToken = default)
    {
        // Create user logic
        return Unit.Value;
    }
}

// Dispatch
await _dispatcher.DispatchAsync(new CreateUserCommand("John", "john@example.com"));
```

## Automatic Caching

Enable caching for frequently-accessed queries:

```csharp
// 1. Register caching services
builder.Services.AddDistributedMemoryCache(); // or any IDistributedCache
builder.Services.AddAdomCqrs(config => config
    .ScanAssemblyContaining<GetUserByIdQueryHandler>()
    .ConfigureCaching(options =>
    {
        options.KeyPrefix = "myapp:";
        options.DefaultCacheDuration = TimeSpan.FromMinutes(5);
    }));

// 2. Implement ICacheableRequest
public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>, ICacheableRequest
{
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(10);
    public string? CacheKey => $"user:{UserId}"; // Optional custom key
}
```

Cached queries are automatically:
- Stored on first execution
- Retrieved from cache on subsequent calls
- Expired after the specified duration
- Bypassed when cache is unavailable (graceful degradation)

## Custom Pipeline Behaviors

Add cross-cutting concerns to the request pipeline:

```csharp
// 1. Create a behavior
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async ValueTask<TResponse> HandleAsync(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Handling {RequestType}", typeof(TRequest).Name);
        var response = await next();
        _logger.LogInformation("Handled {RequestType}", typeof(TRequest).Name);
        return response;
    }
}

// 2. Register the behavior
builder.Services.AddAdomCqrs(config => config
    .ScanAssemblyContaining<GetUserByIdQueryHandler>()
    .AddBehavior<LoggingBehavior<,>>()); // Open generic registration
```

Behaviors execute in registration order around the handler.

## Performance

- **Dispatch overhead**: < 1ms per request (see benchmarks)
- **Zero allocations** on hot paths after warmup
- **O(1) handler lookup** with FrozenDictionary
- **Aggressive inlining** on critical paths

Run benchmarks:
```bash
dotnet run -c Release --project Adom.CQRS.Benchmarks
```

## Dependencies

- Microsoft.Extensions.DependencyInjection.Abstractions (10.0.0)
- Microsoft.Extensions.Caching.Abstractions (10.0.0) - for caching
- System.IO.Hashing (10.0.0) - for cache key generation

## License

MIT

## Contributing

Contributions are welcome! Please see [CONTRIBUTING.md](../../CONTRIBUTING.md) for guidelines.

## Documentation

- [Quickstart Guide](https://github.com/wcontayon/Adom.Framework/tree/main/specs/001-cqrs-library/quickstart.md)
- [Architecture Overview](https://github.com/wcontayon/Adom.Framework/tree/main/specs/001-cqrs-library/plan.md)
- [API Reference](https://github.com/wcontayon/Adom.Framework/tree/main/specs/001-cqrs-library/contracts/)
