// <copyright file="ClientStoreCache.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Caches
{
    using Entities;
    using IdentityServer3.Core.Models;
    using Interfaces;

    internal sealed class ClientStoreCache : RedisCacheBase<Client>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientStoreCache"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="cacheConfiguration">The cache configuration.</param>
        public ClientStoreCache(ICacheManager<Client> cacheManager, IConfiguration<RedisCacheConfigurationEntity> cacheConfiguration)
            : base(cacheManager, cacheConfiguration)
        {
        }
    }
}