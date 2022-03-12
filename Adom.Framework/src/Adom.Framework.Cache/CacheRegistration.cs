using Microsoft.Extensions.DependencyInjection;
using System;

namespace Adom.Framework.Cache
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extensions for registering Adom.Framework.Cache
    /// </summary>
    public static class CacheRegistration
    {
        /// <summary>
        /// Register a <see cref="ICacheStore"/> in <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cacheStoreProvider"></param>
        /// <returns></returns>
        public static IServiceCollection AddCacheStore(this IServiceCollection services, Func<IServiceProvider, ICacheStore> cacheStoreProvider)
        {
            services.AddSingleton(cacheStoreProvider);
            return services;
        }

        /// <summary>
        /// Register a <see cref="ICacheStore"/> in <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cacheStore"></param>
        /// <returns></returns>
        public static IServiceCollection AddCacheStore(this IServiceCollection services, ICacheStore cacheStore)
        {
            services.AddSingleton<ICacheStore>(cacheStore);
            return services;
        }

        /// <summary>
        /// Register a <see cref="ICache"/> in <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                ICacheStore cacheStore = provider!.GetService<ICacheStore>()!;
                if (cacheStore == null) throw new CacheRegistrationException("No cache store registered. Please register a cache store before");

                Cache cache = new Cache(cacheStore);
                return cache;
            });

            return services;
        }

        /// <summary>
        /// Register a <see cref="ICache"/> in <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cacheStore"></param>
        /// <returns></returns>
        public static IServiceCollection AddCache(this IServiceCollection services, ICacheStore cacheStore)
        {
            AddCacheStore(services, cacheStore);
            return AddCache(services);
        }

        /// <summary>
        /// Register a <see cref="ICache"/> in <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cacheStoreProvider"></param>
        /// <returns></returns>
        public static IServiceCollection AddCache(this IServiceCollection service, Func<IServiceProvider, ICacheStore> cacheStoreProvider)
        {
            AddCacheStore(service, cacheStoreProvider);
            return AddCache(service);
        }
    }
}
