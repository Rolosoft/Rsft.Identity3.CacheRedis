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

// <copyright file="RedisStoreBase.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;
    using IdentityServer3.Core.Models;
    using IdentityServer3.Core.Services;
    using Interfaces;

    /// <summary>
    /// The Token Store Base
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IdentityServer3.Core.Services.ITransientDataRepository{TEntity}" />
    public abstract class RedisStoreBase<TEntity> : ITransientDataRepository<TEntity>
        where TEntity : ITokenMetadata
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager<TEntity> cacheManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisStoreBase{TEntity}" /> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        internal RedisStoreBase(ICacheManager<TEntity> cacheManager)
        {
            Contract.Requires(cacheManager != null);

            this.cacheManager = cacheManager;
        }

        /// <summary>
        /// Sets the expiry time.
        /// </summary>
        /// <value>
        /// The expiry time.
        /// </value>
        protected int ExpiryTime { private get; set; }

        /// <summary>
        /// Retrieves all data for a subject identifier.
        /// </summary>
        /// <param name="subject">The subject identifier.</param>
        /// <returns>
        /// A list of token metadata
        /// </returns>
        /// <exception cref="NotImplementedException">Method is not implemented due to lack of ability to perform queries other than get by ID in REDIS</exception>
        public virtual Task<IEnumerable<ITokenMetadata>> GetAllAsync(string subject)
        {
            throw new NotImplementedException("Method is not implemented due to lack of ability to perform queries other than get by ID in REDIS");
        }

        /// <summary>
        /// Retrieves the data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The <see cref="TEntity"/></returns>
        public virtual async Task<TEntity> GetAsync(string key)
        {
            var cacheKey = this.PrefixKey(key);

            return await this.cacheManager.GetAsync(cacheKey).ConfigureAwait(false);
        }

        /// <summary>
        /// Removes the data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The <see cref="Task"/></returns>
        public virtual async Task RemoveAsync(string key)
        {
            var cacheKey = this.PrefixKey(key);

            await this.cacheManager.DeleteAsync(cacheKey).ConfigureAwait(false);
        }

        /// <summary>
        /// Revokes all data for a client and subject id combination.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="client">The client.</param>
        /// <returns>The <see cref="Task"/></returns>
        /// <exception cref="NotImplementedException">Method is not implemented due to lack of ability to perform queries other than get by ID in REDIS</exception>
        public virtual Task RevokeAsync(string subject, string client)
        {
            throw new NotImplementedException("Method is not implemented due to lack of ability to perform queries other than get by ID in REDIS");
        }

        /// <summary>
        /// Stores the data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Task"/></returns>
        public virtual async Task StoreAsync(string key, TEntity value)
        {
            var cacheKey = this.PrefixKey(key);
            var timeSpan = TimeSpan.FromSeconds(this.ExpiryTime);

            await this.cacheManager.SetAsync(cacheKey, value, timeSpan).ConfigureAwait(false);
        }

        /// <summary>
        /// Prefixes the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The <see cref="PrefixKey"/></returns>
        protected abstract string PrefixKey(string key);
    }
}