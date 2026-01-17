namespace Adom.CQRS.Abstractions;

/// <summary>
/// Marker interface that enables response caching for a request.
/// Requests implementing this interface will have their responses automatically cached
/// using <see cref="Microsoft.Extensions.Caching.Distributed.IDistributedCache"/>.
/// </summary>
public interface ICacheableRequest
{
    /// <summary>
    /// Gets the duration to cache the response.
    /// </summary>
    TimeSpan CacheDuration { get; }

    /// <summary>
    /// Gets the optional custom cache key.
    /// If null, a default key is generated using: {TypeName}:{XxHash128(JsonSerialize(request))}.
    /// </summary>
    string? CacheKey => null;
}
