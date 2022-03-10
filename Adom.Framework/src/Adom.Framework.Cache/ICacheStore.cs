using System;
using System.Threading.Tasks;

namespace Adom.Framework.Cache
{
    /// <summary>
    /// Represents a data store used to cache data.
    /// </summary>
    public interface ICacheStore : IAsyncDisposable, IDisposable
    {
        Task<bool> HasKeyAsync(string key);

        Task<bool> TryGetValueAsync<T>(string key, out T data);

        Task<bool> TryGetValueAsync(string key, out object data);

        Task<T> GetEntry<T>(string key);

        Task<T> SetEntryAsync<T>(string key, T data);

        Task<object> SetEntryAsync(string key, object? data);

        Task<bool> RemoveAsync(string key);
    }
}
