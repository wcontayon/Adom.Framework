namespace Adom.CQRS.Caching;

/// <summary>
/// Configuration options for CQRS caching behavior.
/// </summary>
public sealed class CacheOptions
{
    /// <summary>
    /// Gets or sets the default cache duration when not specified by the request.
    /// Default is 5 minutes.
    /// </summary>
    public TimeSpan DefaultCacheDuration { get; set; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Gets or sets the cache key prefix for all cached requests.
    /// Default is "adom:cqrs:".
    /// </summary>
    public string KeyPrefix { get; set; } = "adom:cqrs:";

    /// <summary>
    /// Gets or sets whether caching is enabled.
    /// Default is true.
    /// </summary>
    public bool Enabled { get; set; } = true;
}
