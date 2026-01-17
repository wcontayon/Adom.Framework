using Adom.CQRS.Abstractions;
using Adom.CQRS.Extensions;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Adom.CQRS.Benchmarks;

[MemoryDiagnoser]
[ShortRunJob]
public class CachingBenchmarks
{
    private IServiceProvider _serviceProvider = null!;
    private IDispatcher _dispatcher = null!;
    private CachedQuery _cachedQuery = null!;
    private NonCachedQuery _nonCachedQuery = null!;

    [GlobalSetup]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddDistributedMemoryCache();
        services.AddAdomCqrs(builder =>
        {
            builder.ScanAssemblyContaining<CachingBenchmarks>();
            builder.ConfigureCaching();
        });

        _serviceProvider = services.BuildServiceProvider();
        _dispatcher = _serviceProvider.GetRequiredService<IDispatcher>();

        _cachedQuery = new CachedQuery(42);
        _nonCachedQuery = new NonCachedQuery(42);
    }

    [IterationSetup]
    public void IterationSetup()
    {
        // Warm up cache for cached query benchmarks
        _ = _dispatcher.DispatchAsync<CachedQuery, int>(_cachedQuery).AsTask().Result;
    }

    [Benchmark(Description = "Cache Hit (from cache)")]
    public async Task<int> CacheHit()
    {
        return await _dispatcher.DispatchAsync<CachedQuery, int>(_cachedQuery);
    }

    [Benchmark(Baseline = true, Description = "Non-Cached Query")]
    public async Task<int> NonCachedQuery()
    {
        return await _dispatcher.DispatchAsync<NonCachedQuery, int>(_nonCachedQuery);
    }

    [Benchmark(Description = "Cache Miss (first call)")]
    public async Task<int> CacheMiss()
    {
        // Use a unique query each time to force cache miss
        var query = new CachedQuery(Random.Shared.Next());
        return await _dispatcher.DispatchAsync<CachedQuery, int>(query);
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
public record CachedQuery(int Value) : IRequest<int>, ICacheableRequest
{
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(5);
}

public class CachedQueryHandler : IHandler<CachedQuery, int>
{
    public ValueTask<int> HandleAsync(CachedQuery request, CancellationToken cancellationToken = default)
    {
        // Simulate some work
        return ValueTask.FromResult(request.Value * 2);
    }
}

public record NonCachedQuery(int Value) : IRequest<int>;

public class NonCachedQueryHandler : IHandler<NonCachedQuery, int>
{
    public ValueTask<int> HandleAsync(NonCachedQuery request, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(request.Value * 2);
    }
}
