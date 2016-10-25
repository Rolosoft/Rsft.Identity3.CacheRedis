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

// <copyright file="RefreshTokenStore.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Stores
{
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;
    using Entities;
    using Helpers;
    using IdentityServer3.Core.Models;
    using IdentityServer3.Core.Services;
    using Interfaces;

    /// <summary>
    /// The Refresh Token Store
    /// </summary>
    /// <seealso cref="IdentityServer3.Core.Services.IRefreshTokenStore" />
    public sealed class RefreshTokenStore : RedisStoreBase<RefreshToken>, IRefreshTokenStore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshTokenStore"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="cacheConfiguration">The cache configuration.</param>
        internal RefreshTokenStore(
            ICacheManager<RefreshToken> cacheManager,
            IConfiguration<RedisCacheConfigurationEntity> cacheConfiguration)
            : base(cacheManager, cacheConfiguration)
        {
            Contract.Requires(cacheManager != null);
            Contract.Requires(cacheConfiguration != null);
        }

        /// <summary>
        /// Stores the data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="T:System.Threading.Tasks.Task" />
        /// </returns>
        public override Task StoreAsync(string key, RefreshToken value)
        {
            var timespan = CacheHelpers.GetCacheTimeSpan(value.LifeTime);
            return this.StoreAsync(key, value, timespan);
        }

        /// <summary>
        /// Prefixes the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The Prefixed Key</returns>
        protected override string PrefixKey(string key)
        {
            return $"RTS_{key}";
        }
    }
}