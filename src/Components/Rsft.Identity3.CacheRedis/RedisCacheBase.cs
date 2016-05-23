﻿// Copyright 2016 Rolosoft Ltd
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// <copyright file="RedisCacheBase.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;
    using Contracts;
    using Entities;
    using IdentityServer3.Core.Services;
    using Interfaces;

    [ContractClass(typeof(RedisCacheBaseContract<>))]
    internal abstract class RedisCacheBase<T> : ICache<T>
        where T : class
    {
        /// <summary>
        /// Gets the cache manager.
        /// </summary>
        /// <value>
        /// The cache manager.
        /// </value>
        private readonly ICacheManager<T> cacheManager;

        /// <summary>
        /// The cache configuration
        /// </summary>
        private readonly IConfiguration<RedisCacheConfigurationEntity> cacheConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheBase{T}" /> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="cacheConfiguration">The cache configuration.</param>
        protected RedisCacheBase(
            ICacheManager<T> cacheManager,
            IConfiguration<RedisCacheConfigurationEntity> cacheConfiguration)
        {
            Contract.Requires(cacheManager != null);
            Contract.Requires(cacheConfiguration != null);

            this.cacheManager = cacheManager;
            this.cacheConfiguration = cacheConfiguration;
        }

        public virtual async Task<T> GetAsync(string key)
        {
            var foo = await this.cacheManager.GetAsync(key).ConfigureAwait(false);

            return foo;
        }

        public virtual async Task SetAsync(string key, T item)
        {
            var timeSpan = new TimeSpan(0, 0, 0, this.cacheConfiguration.Get.CacheDuration);

            await this.cacheManager.SetAsync(key, item, timeSpan).ConfigureAwait(false);
        }
    }
}