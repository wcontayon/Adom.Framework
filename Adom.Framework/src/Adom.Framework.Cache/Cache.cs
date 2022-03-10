
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Adom.Framework.Cache
{
    /// <summary>
    /// Default <see cref="ICache"/> implementation
    /// using the <see cref="ICacheStore"/>
    /// </summary>
    internal sealed class Cache : ICache, IAsyncDisposable, IDisposable
    {
        private readonly ICacheStore _cacheStore;

        //this is used to track all keys in the cache store
        private readonly HashSet<string> _keys;
        private readonly object _lock = new object();
        private readonly AsyncLock.AsyncLock _asyncLock = new AsyncLock.AsyncLock();
        private bool _disposed;

        public Cache(ICacheStore memoryCache)
        {
            _cacheStore = memoryCache;
            _keys = new HashSet<string>();
        }

        public async Task Clear()
        {
            await using (await _asyncLock.LockAsync().ConfigureAwait(false))
            {
                foreach (string key in _keys)
                {
                    await _cacheStore.RemoveAsync(key).ConfigureAwait(false);
                }

                _keys.Clear();
            }

            await Task.CompletedTask.ConfigureAwait(false);
        }

        #region Dispose

        public void Dispose()
        {
            if (!_disposed)
            {
                _asyncLock.Dispose();
                _cacheStore.Dispose();
                _keys.Clear();
                _disposed = true;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                _asyncLock.Dispose();
                await _cacheStore.DisposeAsync().ConfigureAwait(false);
                _keys.Clear();
                _disposed = true;
            }
        }

        #endregion

        #region Get Methods

        public T Get<T>(string key)
        {
            T data;
            lock (_lock)
            {
                _cacheStore.TryGetValueAsync(key, out data).Wait();
            }

            return data;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            T data;
            Debug.Assert(!string.IsNullOrEmpty(key));
            await using (await _asyncLock.LockAsync().ConfigureAwait(false))
            {
                await _cacheStore.TryGetValueAsync(key, out data).ConfigureAwait(false);
            }

            return data;
        }

        public T GetOrStore<T>(string key, Func<T> getMethod)
        {   
            T data;
            Debug.Assert(getMethod != null);
            Debug.Assert(!string.IsNullOrEmpty(key));
            lock (_lock)
            {
                if (!_cacheStore.TryGetValueAsync(key, out data).Result)
                {
                    data = getMethod();
                    _keys.Add(key);
                    _cacheStore.SetEntryAsync<T>(key, data).Wait();
                }
            }

            return data;
        }

        public async Task<T> GetOrStoreAsync<T>(string key, Func<T> getMethod)
        {
            T data;
            Debug.Assert(getMethod != null);
            Debug.Assert(!string.IsNullOrEmpty(key));
            await using (await _asyncLock.LockAsync().ConfigureAwait(false))
            {
                if (!await _cacheStore.TryGetValueAsync(key, out data).ConfigureAwait(false))
                {
                    data = getMethod();
                    _keys.Add(key);
                    await _cacheStore.SetEntryAsync<T>(key, data).ConfigureAwait(false);
                }
            }

            return data;
        }

        #endregion


        public bool HasKey(string key)
        {
            Debug.Assert(!string.IsNullOrEmpty(key));
            lock (_lock)
            {
                return _keys.Contains(key);
            }
        }

        public Task<bool> HasKeyAsync(string key)
        {
            Debug.Assert(!string.IsNullOrEmpty(key));
            lock (_lock)
            {
                return Task.FromResult<bool>(_keys.Contains(key));
            }
        }

        public bool RemoveKey(string key)
        {
            Debug.Assert(!string.IsNullOrEmpty(key));
            lock (_lock)
            {
                bool result = _cacheStore.RemoveAsync(key).Result;
                if (result) _keys.Remove(key);
                return result;
            }
        }

        public async Task<bool> RemoveKeyAsync(string key)
        {
            Debug.Assert(!string.IsNullOrEmpty(key));
            await using (await _asyncLock.LockAsync().ConfigureAwait(false))
            {
                bool result = await _cacheStore.RemoveAsync(key).ConfigureAwait(false);
                if (result) _keys.Remove(key);
                return result;
            }
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
