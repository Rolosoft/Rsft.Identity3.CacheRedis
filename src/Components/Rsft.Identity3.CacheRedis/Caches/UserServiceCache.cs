// <copyright file="UserServiceCache.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Caches
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Entities;
    using Interfaces;

    internal sealed class UserServiceCache : RedisCacheBase<IEnumerable<Claim>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserServiceCache"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="cacheConfiguration">The cache configuration.</param>
        public UserServiceCache(ICacheManager<IEnumerable<Claim>> cacheManager, IConfiguration<RedisCacheConfigurationEntity> cacheConfiguration)
            : base(cacheManager, cacheConfiguration)
        {
        }
    }
}