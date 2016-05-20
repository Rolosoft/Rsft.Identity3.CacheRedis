// <copyright file="RedisCacheConfigurationEntity.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Entities
{
    public sealed class RedisCacheConfigurationEntity
    {
        /// <summary>
        /// Gets or sets the duration of objects to be stored in the cache (in milliseconds).
        /// </summary>
        /// <value>
        /// The duration of the cache. Setting of 0 places items in cache for infinite duration.
        /// </value>
        /// <remarks>
        /// Default setting is 3600 seconds (1 hour).
        /// </remarks>
        public int CacheDuration { get; set; } = 1000 * 60 * 60;

        /// <summary>
        /// Gets or sets the redis cache default prefix.
        /// </summary>
        /// <value>
        /// The redis cache default prefix.
        /// </value>
        public string RedisCacheDefaultPrefix { get; set; } = @"rsftidcache";
    }
}