using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adom.Framework.Cache
{
    internal interface ICache
    {
        /// <summary>
        /// Retrieve a <typeparamref name="T"/> data from the cache store
        /// associated to the <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The cache key</param>
        /// <returns>The cached data</returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// Retrieve a <typeparamref name="T"/> data from the cache store
        /// associated to the <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The cache key</param>
        /// <returns>The cached data</returns>
        T Get<T>(string key);

        /// <summary>
        /// Retrieve a <typeparamref name="T"/> data from the cache store
        /// associated to the <paramref name="key"/>.
        /// if the key does not exist, the data is retrieved using the <paramref name="getMethod"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The cache key</param>
        /// <param name="getMethod">The method to retrieve the data and store in the cache store</param>
        /// <returns>The cached data</returns>
        Task<T> GetOrStoreAsync<T>(string key, Func<T> getMethod);

        /// <summary>
        /// Retrieve a <typeparamref name="T"/> data from the cache store
        /// associated to the <paramref name="key"/>.
        /// if the key does not exist, the data is retrieved using the <paramref name="getMethod"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The cache key</param>
        /// <param name="getMethod">The method to retrieve the data and store in the cache store</param>
        /// <returns>The cached data</returns>
        T GetOrStore<T>(string key, Func<T> getMethod);

        /// <summary>
        /// Set a <typeparamref name="T"/> data into the cache store
        /// with the <paramref name="key"/> and using the <paramref name="getMethod"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The cache key</param>
        /// <param name="getMethod">The method to retrieve the data and store in the cache store</param>
        Task SetKeyAsync<T>(string key, Func<T> getMethod);

        /// <summary>
        /// Set a <typeparamref name="T"/> data into the cache store
        /// with the <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The cache key</param>
        /// <param name="data">The data to store in the cache store</param>
        Task SetKeyAsync<T>(string key, T data);

        /// <summary>
        /// Set a data into the cache store
        /// with the <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <param name="data">The data to store in the cache store</param>
        Task SetKeyAsync(string key, object? data);

        /// <summary>
        /// Check either the key exist in the cache store
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>True or false</returns>
        Task<bool> HasKeyAsync(string key);

        /// <summary>
        /// Check either the key exist in the cache store
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>True or false</returns>
        bool HasKey(string key);

        /// <summary>
        /// Remove the key and the data cached from the cache store.
        /// </summary>
        /// <param name="key">The key to remove</param>
        /// <returns>True or false (operation success)</returns>
        Task<bool> RemoveKeyAsync(string key);

        /// <summary>
        /// Remove the key and the data cached from the cache store.
        /// </summary>
        /// <param name="key">The key to remove</param>
        /// <returns>True or false (operation success)</returns>
        bool RemoveKey(string key);

        /// <summary>
        /// Clear the cache store, and remove all the datas
        /// </summary>
        Task Clear();
    }
}
