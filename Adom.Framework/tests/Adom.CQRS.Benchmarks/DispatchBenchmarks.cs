using Adom.CQRS.Abstractions;
using Adom.CQRS.Extensions;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Adom.CQRS.Benchmarks;

[MemoryDiagnoser]
[ShortRunJob]
public class DispatchBenchmarks
{
    private IServiceProvider _serviceProvider = null!;
    private IDispatcher _dispatcher = null!;
    private SimpleCommand _command = null!;
    private SimpleQuery _query = null!;

    [GlobalSetup]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder => builder.ScanAssemblyContaining<DispatchBenchmarks>());

        _serviceProvider = services.BuildServiceProvider();
        _dispatcher = _serviceProvider.GetRequiredService<IDispatcher>();

        _command = new SimpleCommand("test");
        _query = new SimpleQuery(42);
    }

    [Benchmark(Description = "Command Dispatch (void return)")]
    public async Task DispatchCommand()
    {
        await _dispatcher.DispatchAsync(_command);
    }

    [Benchmark(Description = "Query Dispatch (with return value)")]
    public async Task<int> DispatchQuery()
    {
        return await _dispatcher.DispatchAsync<SimpleQuery, int>(_query);
    }

    [Benchmark(Description = "Complex Query Dispatch")]
    public async Task<ComplexDto> DispatchComplexQuery()
    {
        var query = new ComplexQuery(Guid.NewGuid(), "test@example.com");
        return await _dispatcher.DispatchAsync<ComplexQuery, ComplexDto>(query);
    }

    [Benchmark(Baseline = true, Description = "Baseline: Direct Handler Invocation")]
    public async Task<int> DirectHandlerInvocation()
    {
        var handler = new SimpleQueryHandler();
        return await handler.HandleAsync(_query, CancellationToken.None);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        if (_serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}

// Benchmark test types
public record SimpleCommand(string Value) : IRequest;

public class SimpleCommandHandler : IHandler<SimpleCommand>
{
    public ValueTask<Unit> HandleAsync(SimpleCommand request, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(Unit.Value);
    }
}

public record SimpleQuery(int Value) : IRequest<int>;

public class SimpleQueryHandler : IHandler<SimpleQuery, int>
{
    public ValueTask<int> HandleAsync(SimpleQuery request, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(request.Value * 2);
    }
}

public record ComplexQuery(Guid Id, string Email) : IRequest<ComplexDto>;

public record ComplexDto(Guid Id, string Email, string Name, DateTime CreatedAt);

public class ComplexQueryHandler : IHandler<ComplexQuery, ComplexDto>
{
    public ValueTask<ComplexDto> HandleAsync(ComplexQuery request, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(new ComplexDto(
            request.Id,
            request.Email,
            "John Doe",
            DateTime.UtcNow
        ));
    }
}
