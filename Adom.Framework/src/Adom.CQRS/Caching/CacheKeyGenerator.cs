using System.Buffers;
using System.IO.Hashing;
using System.Text.Json;
using Adom.CQRS.Abstractions;

namespace Adom.CQRS.Caching;

/// <summary>
/// Generates cache keys for requests using XxHash128 for fast hashing.
/// </summary>
internal static class CacheKeyGenerator
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = false,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// Generates a cache key for the specified request.
    /// If the request implements <see cref="ICacheableRequest"/> with a custom CacheKey, that key is used.
    /// Otherwise, a key is generated as: {prefix}{TypeName}:{XxHash128(JsonSerialize(request))}.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <param name="request">The request instance.</param>
    /// <param name="prefix">The cache key prefix.</param>
    /// <returns>A unique cache key for the request.</returns>
    public static string GenerateKey<TRequest>(TRequest request, string prefix)
        where TRequest : notnull
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(prefix);

        // Check if request provides a custom cache key
        if (request is ICacheableRequest cacheable && cacheable.CacheKey is not null)
        {
            return $"{prefix}{cacheable.CacheKey}";
        }

        // Generate default key: {prefix}{TypeName}:{Hash}
        var typeName = typeof(TRequest).Name;
        var hash = ComputeHash(request);

        return $"{prefix}{typeName}:{hash}";
    }

    private static string ComputeHash<TRequest>(TRequest request)
    {
        // Serialize request to JSON bytes
        var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(request, SerializerOptions);

        // Compute XxHash128
        var hashBytes = XxHash128.Hash(jsonBytes);

        // Convert to hex string (32 characters for 128-bit hash)
#pragma warning disable CA1308 // Lowercase is intentional for cache key consistency
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
#pragma warning restore CA1308
    }
}
