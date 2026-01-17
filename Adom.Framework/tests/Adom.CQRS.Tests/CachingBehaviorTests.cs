using Adom.CQRS.Abstractions;
using Adom.CQRS.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Adom.CQRS.Tests;

public class CachingBehaviorTests
{
    [Fact]
    public async Task CacheableRequest_FirstCall_ShouldInvokeHandlerAndCacheResult()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddDistributedMemoryCache();
        services.AddAdomCqrs(builder =>
        {
            builder.ScanAssemblyContaining<CachingBehaviorTests>();
            builder.ConfigureCaching();
        });
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var query = new CacheableTestQuery(42);

        // Reset handler invocation counter
        CacheableTestQueryHandler.InvocationCount = 0;

        // Act
        var result = await dispatcher.DispatchAsync<CacheableTestQuery, int>(query);

        // Assert
        result.Should().Be(84); // 42 * 2
        CacheableTestQueryHandler.InvocationCount.Should().Be(1);
    }

    [Fact]
    public async Task CacheableRequest_SecondCall_ShouldReturnCachedResult()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddDistributedMemoryCache();
        services.AddAdomCqrs(builder =>
        {
            builder.ScanAssemblyContaining<CachingBehaviorTests>();
            builder.ConfigureCaching();
        });
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var query = new CacheableTestQuery(42);

        // Act - First call
        var result1 = await dispatcher.DispatchAsync<CacheableTestQuery, int>(query);

        // Reset handler invocation counter
        CacheableTestQueryHandler.InvocationCount = 0;

        // Act - Second call (should be from cache)
        var result2 = await dispatcher.DispatchAsync<CacheableTestQuery, int>(query);

        // Assert
        result1.Should().Be(84);
        result2.Should().Be(84);
        CacheableTestQueryHandler.InvocationCount.Should().Be(0); // Handler not invoked on second call
    }

    [Fact]
    public async Task NonCacheableRequest_ShouldBypassCache()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddDistributedMemoryCache();
        services.AddAdomCqrs(builder =>
        {
            builder.ScanAssemblyContaining<CachingBehaviorTests>();
            builder.ConfigureCaching();
        });
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var query = new NonCacheableTestQuery(42);

        // Reset handler invocation counter
        NonCacheableTestQueryHandler.InvocationCount = 0;

        // Act - Call twice
        var result1 = await dispatcher.DispatchAsync<NonCacheableTestQuery, int>(query);
        var result2 = await dispatcher.DispatchAsync<NonCacheableTestQuery, int>(query);

        // Assert
        result1.Should().Be(84);
        result2.Should().Be(84);
        NonCacheableTestQueryHandler.InvocationCount.Should().Be(2); // Handler invoked both times
    }

    [Fact]
    public async Task CacheableRequest_WithoutDistributedCache_ShouldFallbackToHandler()
    {
        // Arrange - No IDistributedCache registered
        var services = new ServiceCollection();
        services.AddAdomCqrs(builder =>
        {
            builder.ScanAssemblyContaining<CachingBehaviorTests>();
            builder.ConfigureCaching();
        });
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var query = new CacheableTestQuery(42);

        // Reset handler invocation counter
        CacheableTestQueryHandler.InvocationCount = 0;

        // Act - Call twice
        var result1 = await dispatcher.DispatchAsync<CacheableTestQuery, int>(query);
        var result2 = await dispatcher.DispatchAsync<CacheableTestQuery, int>(query);

        // Assert - Handler should be invoked both times since cache is unavailable
        result1.Should().Be(84);
        result2.Should().Be(84);
        CacheableTestQueryHandler.InvocationCount.Should().Be(2);
    }

    [Fact]
    public async Task CacheableRequest_WithCustomCacheKey_ShouldUseCustomKey()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddDistributedMemoryCache();
        services.AddAdomCqrs(builder =>
        {
            builder.ScanAssemblyContaining<CachingBehaviorTests>();
            builder.ConfigureCaching();
        });
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();
        var cache = serviceProvider.GetRequiredService<IDistributedCache>();

        var query = new CustomKeyCacheableQuery(42, "my-custom-key");

        // Act
        var result = await dispatcher.DispatchAsync<CustomKeyCacheableQuery, int>(query);

        // Assert
        result.Should().Be(84);

        // Verify cache contains the result with custom key
        var cachedBytes = await cache.GetAsync("adom:cqrs:my-custom-key");
        cachedBytes.Should().NotBeNull();
    }

    [Fact]
    public async Task CacheableRequest_WithCustomCacheDuration_ShouldRespectDuration()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddDistributedMemoryCache();
        services.AddAdomCqrs(builder =>
        {
            builder.ScanAssemblyContaining<CachingBehaviorTests>();
            builder.ConfigureCaching();
        });
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        // Query with very short cache duration
        var query = new ShortCacheDurationQuery(42);

        // Act - First call
        var result1 = await dispatcher.DispatchAsync<ShortCacheDurationQuery, int>(query);

        // Reset handler invocation counter
        ShortCacheDurationQueryHandler.InvocationCount = 0;

        // Wait for cache to expire (10ms duration + buffer)
        await Task.Delay(50);

        // Act - Second call (cache should have expired)
        var result2 = await dispatcher.DispatchAsync<ShortCacheDurationQuery, int>(query);

        // Assert
        result1.Should().Be(84);
        result2.Should().Be(84);
        ShortCacheDurationQueryHandler.InvocationCount.Should().Be(1); // Handler invoked after expiration
    }

    [Fact]
    public async Task ConfigureCaching_WithCustomOptions_ShouldUseCustomPrefix()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddDistributedMemoryCache();
        services.AddAdomCqrs(builder =>
        {
            builder.ScanAssemblyContaining<CachingBehaviorTests>();
            builder.ConfigureCaching(options =>
            {
                options.KeyPrefix = "myapp:custom:";
            });
        });
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var query = new CacheableTestQuery(42);

        // Reset handler invocation counter
        CacheableTestQueryHandler.InvocationCount = 0;

        // Act - Call twice
        var result1 = await dispatcher.DispatchAsync<CacheableTestQuery, int>(query);
        CacheableTestQueryHandler.InvocationCount = 0;
        var result2 = await dispatcher.DispatchAsync<CacheableTestQuery, int>(query);

        // Assert - Second call should be cached (handler not invoked)
        result1.Should().Be(84);
        result2.Should().Be(84);
        CacheableTestQueryHandler.InvocationCount.Should().Be(0); // Handler not invoked on second call
    }

    [Fact]
    public async Task ConfigureCaching_WithDisabledCaching_ShouldNotCache()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddDistributedMemoryCache();
        services.AddAdomCqrs(builder =>
        {
            builder.ScanAssemblyContaining<CachingBehaviorTests>();
            builder.ConfigureCaching(options =>
            {
                options.Enabled = false;
            });
        });
        var serviceProvider = services.BuildServiceProvider();
        var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

        var query = new CacheableTestQuery(42);

        // Reset handler invocation counter
        CacheableTestQueryHandler.InvocationCount = 0;

        // Act - Call twice
        var result1 = await dispatcher.DispatchAsync<CacheableTestQuery, int>(query);
        var result2 = await dispatcher.DispatchAsync<CacheableTestQuery, int>(query);

        // Assert - Handler should be invoked both times since caching is disabled
        result1.Should().Be(84);
        result2.Should().Be(84);
        CacheableTestQueryHandler.InvocationCount.Should().Be(2);
    }
}

// Test queries and handlers
public record CacheableTestQuery(int Value) : IRequest<int>, ICacheableRequest
{
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(5);
}

public class CacheableTestQueryHandler : IHandler<CacheableTestQuery, int>
{
    public static int InvocationCount;

    public ValueTask<int> HandleAsync(CacheableTestQuery request, CancellationToken cancellationToken = default)
    {
        InvocationCount++;
        return ValueTask.FromResult(request.Value * 2);
    }
}

public record NonCacheableTestQuery(int Value) : IRequest<int>;

public class NonCacheableTestQueryHandler : IHandler<NonCacheableTestQuery, int>
{
    public static int InvocationCount;

    public ValueTask<int> HandleAsync(NonCacheableTestQuery request, CancellationToken cancellationToken = default)
    {
        InvocationCount++;
        return ValueTask.FromResult(request.Value * 2);
    }
}

public record CustomKeyCacheableQuery(int Value, string CustomKey) : IRequest<int>, ICacheableRequest
{
    public TimeSpan CacheDuration => TimeSpan.FromMinutes(5);
    public string? CacheKey => CustomKey;
}

public class CustomKeyCacheableQueryHandler : IHandler<CustomKeyCacheableQuery, int>
{
    public ValueTask<int> HandleAsync(CustomKeyCacheableQuery request, CancellationToken cancellationToken = default)
    {
        return ValueTask.FromResult(request.Value * 2);
    }
}

public record ShortCacheDurationQuery(int Value) : IRequest<int>, ICacheableRequest
{
    public TimeSpan CacheDuration => TimeSpan.FromMilliseconds(10);
}

public class ShortCacheDurationQueryHandler : IHandler<ShortCacheDurationQuery, int>
{
    public static int InvocationCount;

    public ValueTask<int> HandleAsync(ShortCacheDurationQuery request, CancellationToken cancellationToken = default)
    {
        InvocationCount++;
        return ValueTask.FromResult(request.Value * 2);
    }
}
