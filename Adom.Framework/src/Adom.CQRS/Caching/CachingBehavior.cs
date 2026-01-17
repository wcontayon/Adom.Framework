using System.Text.Json;
using Adom.CQRS.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Adom.CQRS.Caching;

/// <summary>
/// Pipeline behavior that automatically caches responses for requests implementing <see cref="ICacheableRequest"/>.
/// Uses <see cref="IDistributedCache"/> for cache storage with configurable options.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
#pragma warning disable CA1812 // Internal class is instantiated via DI container
#pragma warning disable CA1848 // Logging performance - acceptable for caching behavior
internal sealed class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IDistributedCache? _cache;
    private readonly CacheOptions _options;
    private readonly ILogger<CachingBehavior<TRequest, TResponse>>? _logger;

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = false
    };

    public CachingBehavior(IServiceProvider serviceProvider, IOptions<CacheOptions> options)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(options);
        _cache = serviceProvider.GetService<IDistributedCache>();
        _options = options.Value;
        _logger = serviceProvider.GetService<ILogger<CachingBehavior<TRequest, TResponse>>>();
    }

    public async ValueTask<TResponse> HandleAsync(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken = default)
    {
        // Skip caching if not enabled or request doesn't implement ICacheableRequest
        if (!_options.Enabled || request is not ICacheableRequest cacheable)
        {
            return await next().ConfigureAwait(false);
        }

        // Skip caching if cache is unavailable
        if (_cache is null)
        {
            _logger?.LogWarning("IDistributedCache not registered, skipping cache for {RequestType}", typeof(TRequest).Name);
            return await next().ConfigureAwait(false);
        }

        var cacheKey = CacheKeyGenerator.GenerateKey(request, _options.KeyPrefix);

        try
        {
            // Try to get cached response
            var cachedBytes = await _cache.GetAsync(cacheKey, cancellationToken).ConfigureAwait(false);
            if (cachedBytes is not null)
            {
                _logger?.LogDebug("Cache hit for key: {CacheKey}", cacheKey);
                var cachedResponse = JsonSerializer.Deserialize<TResponse>(cachedBytes, SerializerOptions);
                if (cachedResponse is not null)
                {
                    return cachedResponse;
                }

                _logger?.LogWarning("Failed to deserialize cached response for key: {CacheKey}", cacheKey);
            }
        }
#pragma warning disable CA1031 // Generic exception catch is intentional for graceful degradation
        catch (Exception ex)
#pragma warning restore CA1031
        {
            // Log error but continue to handler execution (graceful degradation)
            _logger?.LogError(ex, "Error retrieving from cache for key: {CacheKey}", cacheKey);
        }

        // Cache miss or error - execute handler
        _logger?.LogDebug("Cache miss for key: {CacheKey}", cacheKey);
        var response = await next().ConfigureAwait(false);

        // Cache the response
        try
        {
            var responseBytes = JsonSerializer.SerializeToUtf8Bytes(response, SerializerOptions);
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheable.CacheDuration
            };

            await _cache.SetAsync(cacheKey, responseBytes, cacheOptions, cancellationToken).ConfigureAwait(false);
            _logger?.LogDebug("Cached response for key: {CacheKey} with duration: {Duration}", cacheKey, cacheable.CacheDuration);
        }
#pragma warning disable CA1031 // Generic exception catch is intentional for graceful degradation
        catch (Exception ex)
#pragma warning restore CA1031
        {
            // Log error but return response (caching failure should not affect functionality)
            _logger?.LogError(ex, "Error caching response for key: {CacheKey}", cacheKey);
        }

        return response;
    }
}
#pragma warning restore CA1848
#pragma warning restore CA1812
