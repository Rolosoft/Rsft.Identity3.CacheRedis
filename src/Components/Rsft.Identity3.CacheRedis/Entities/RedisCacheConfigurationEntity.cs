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

        /// <summary>
        /// Gets or sets a value indicating whether to use compression to store objects in Redis.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use object compression]; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        /// <para>Compression can dramatically increase storage efficiency in Redis. Store more objects for less cost.</para>
        /// <para>Default is 'True'.</para>
        /// </remarks>
        public bool UseObjectCompression { get; set; } = true;
    }
}