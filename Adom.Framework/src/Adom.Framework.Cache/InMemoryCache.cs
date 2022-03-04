using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adom.Framework.Cache
{
    /// <summary>
    /// Default <see cref="ICache"/> implementation
    /// using the <see cref="IMemoryCache"/> as a cache store
    /// </summary>
    public sealed class InMemoryCache : ICache
    {
        private readonly IMemoryCache _cacheStore;

        //this is used to track all keys in the cache store
        private readonly HashSet<string> _keys;
        private readonly object _lock = new object();

        public InMemoryCache(IMemoryCache memoryCache)
        {
            _cacheStore = memoryCache;
            _keys = new HashSet<string>();
        }

        public Task Clear()
        {
            lock(_lock)
            {
                foreach(string key in _keys)
                {
                    _cacheStore.Remove(key);
                }

                _keys.Clear();
            }

            return Task.CompletedTask;
        }

        #region Get Methods

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public T GetOrStore<T>(string key, Func<T> getMethod)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOrStoreAsync<T>(string key, Func<T> getMethod)
        {
            throw new NotImplementedException();
        }

        #endregion


        public bool HasKey(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public bool RemoveKey(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task SetKeyAsync<T>(string key, Func<T> getMethod)
        {
            throw new NotImplementedException();
        }

        public Task SetKeyAsync<T>(string key, T data)
        {
            throw new NotImplementedException();
        }

        public Task SetKeyAsync<T>(string key, object? data)
        {
            throw new NotImplementedException();
        }
    }
}
