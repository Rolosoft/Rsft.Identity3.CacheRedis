// Copyright 2016 Rolosoft Ltd
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// <copyright file="RedisCacheConfigurationEntity.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Entities
{
    /// <summary>
    /// The Redis cache configuration entity.
    /// </summary>
    public sealed class RedisCacheConfigurationEntity
    {
        /// <summary>
        /// Gets or sets the duration of objects to be stored in the cache (in seconds).
        /// </summary>
        /// <value>
        /// The duration of the cache. Setting of 0 places items in cache for infinite duration.
        /// </value>
        /// <remarks>
        /// <para>
        /// Default setting is 3600 seconds (1 hour).
        /// </para>
        /// </remarks>
        public int CacheDuration { get; set; } = 60 * 60;

        /// <summary>
        /// Gets or sets the redis cache default prefix.
        /// </summary>
        /// <value>
        /// The redis cache default prefix.
        /// </value>
        /// <remarks>
        /// <para>Default is rsftid3cache.</para>
        /// </remarks>
        public string RedisCacheDefaultPrefix { get; set; } = @"rsftid3cache";

        /// <summary>
        /// Gets or sets the duration of the refresh token cache.
        /// </summary>
        /// <value>
        /// The duration of the refresh token cache.
        /// </value>
        public int RefreshTokenCacheDuration { get; set; } = 60 * 60 * 3;

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