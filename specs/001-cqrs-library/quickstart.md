# Quickstart: Adom.CQRS

**Version**: 1.0.0
**Target**: .NET 10+

## Installation

```bash
dotnet add package Adom.CQRS
```

For compile-time handler discovery (optional, recommended for Native AOT):
```bash
dotnet add package Adom.CQRS.SourceGenerators
```

---

## Basic Usage

### 1. Define a Query

```csharp
// A query that returns a user by ID
public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;

public record UserDto(Guid Id, string Name, string Email);
```

### 2. Define a Handler

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

---

## Commands (No Return Value)

### Define a Command

```csharp
public record CreateUserCommand(string Name, string Email) : IRequest;
```

### Define a Handler

```csharp
public class CreateUserCommandHandler : IHandler<CreateUserCommand>
{
    private readonly IUserRepository _repository;

    public CreateUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<Unit> HandleAsync(
        CreateUserCommand request,
        CancellationToken cancellationToken = default)
    {
        await _repository.CreateAsync(new User(request.Name, request.Email), cancellationToken);
        return Unit.Value;
    }
}
```

### Dispatch

```csharp
await _dispatcher.DispatchAsync(new CreateUserCommand("John", "john@example.com"), ct);
```

---

## Caching

### Enable Caching on a Query

```csharp
public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>, ICacheableRequest
{
    // Cache for 10 minutes
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(10);

    // Optional: custom cache key (null = auto-generated)
    public string? CacheKey => null;
}
```

### Configure Caching

```csharp
builder.Services.AddAdomCqrs(config => config
    .ScanAssemblyContaining<GetUserByIdQueryHandler>()
    .ConfigureCaching(cache =>
    {
        cache.DefaultDuration = TimeSpan.FromMinutes(5);
        cache.KeyPrefix = "myapp:";
    }));

// Requires IDistributedCache to be registered
builder.Services.AddDistributedMemoryCache(); // or Redis, SQL, etc.
```

---

## Pipeline Behaviors

### Create a Logging Behavior

```csharp
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
```

### Register the Behavior

```csharp
builder.Services.AddAdomCqrs(config => config
    .ScanAssemblyContaining<GetUserByIdQueryHandler>()
    .AddBehavior<LoggingBehavior<,>>());
```

---

## Source Generators (Native AOT)

For zero-reflection handler discovery:

```csharp
// Install Adom.CQRS.SourceGenerators package
// Then use generated registration:

builder.Services.AddAdomCqrs(config => config
    .UseGeneratedRegistration()); // Uses compile-time discovered handlers
```

The source generator creates a `AdomCqrsRegistration` class at compile time with all handlers pre-registered.

---

## Error Handling

### Handler Not Found

```csharp
try
{
    await _dispatcher.DispatchAsync<UnregisteredQuery, Result>(query, ct);
}
catch (HandlerNotFoundException ex)
{
    // ex.RequestType contains the unregistered request type
    _logger.LogError("No handler for {Type}", ex.RequestType.Name);
}
```

### Dispatcher Errors

```csharp
try
{
    await _dispatcher.DispatchAsync<MyQuery, Result>(query, ct);
}
catch (DispatcherException ex)
{
    // ex.InnerException contains the actual error
    _logger.LogError(ex.InnerException, "Dispatch failed for {Type}", ex.RequestType.Name);
}
```

---

## Performance Tips

1. **Use `ValueTask<T>`**: All handlers return `ValueTask<T>` to avoid allocations for sync completions
2. **Enable Source Generators**: For Native AOT and zero-reflection startup
3. **Implement `ICacheableRequest`**: For frequently-accessed read queries
4. **Keep Handlers Stateless**: Registered as transient, should not hold state between calls

---

## Complete Example

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAdomCqrs(config => config
    .ScanAssemblyContaining<Program>()
    .AddBehavior<LoggingBehavior<,>>()
    .ConfigureCaching(cache => cache.DefaultDuration = TimeSpan.FromMinutes(5)));

builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

app.MapGet("/users/{id}", async (Guid id, IDispatcher dispatcher, CancellationToken ct) =>
{
    var user = await dispatcher.DispatchAsync<GetUserByIdQuery, UserDto>(
        new GetUserByIdQuery(id), ct);
    return Results.Ok(user);
});

app.Run();
```
