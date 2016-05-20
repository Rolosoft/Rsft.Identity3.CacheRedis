// <copyright file="RedisCacheBase.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Threading;
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
            var foo = await this.cacheManager.GetAsync(key, CancellationToken.None).ConfigureAwait(false);

            return foo;
        }

        public virtual async Task SetAsync(string key, T item)
        {
            var addMilliseconds = DateTime.UtcNow.AddMilliseconds(this.cacheConfiguration.Get.CacheDuration);

            var specifyKind = DateTime.SpecifyKind(addMilliseconds, DateTimeKind.Utc);

            var dateTimeOffset = new DateTimeOffset(specifyKind);

            await this.cacheManager.SetAsync(key, item, dateTimeOffset, CancellationToken.None).ConfigureAwait(false);
        }
    }
}