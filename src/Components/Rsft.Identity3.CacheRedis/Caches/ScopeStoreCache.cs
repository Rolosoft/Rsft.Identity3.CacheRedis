// <copyright file="ScopeStoreCache.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Caches
{
    using System.Collections.Generic;

    using Entities;
    using IdentityServer3.Core.Models;
    using Interfaces;

    internal sealed class ScopeStoreCache : RedisCacheBase<IEnumerable<Scope>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScopeStoreCache"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="cacheConfiguration">The cache configuration.</param>
        public ScopeStoreCache(ICacheManager<IEnumerable<Scope>> cacheManager, IConfiguration<RedisCacheConfigurationEntity> cacheConfiguration)
            : base(cacheManager, cacheConfiguration)
        {
        }
    }
}