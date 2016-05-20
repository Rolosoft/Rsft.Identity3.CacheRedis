// <copyright file="RedisCacheBaseContract.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Contracts
{
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;

    [ContractClassFor(typeof(RedisCacheBase<>))]
    internal abstract class RedisCacheBaseContract<T> : RedisCacheBase<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheBaseContract{T}"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="cacheConfiguration">The cache configuration.</param>
        protected RedisCacheBaseContract(ICacheManager<T> cacheManager, IConfiguration<RedisCacheConfigurationEntity> cacheConfiguration)
            : base(cacheManager, cacheConfiguration)
        {
        }

        public override Task<T> GetAsync(string key)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(key));
            return base.GetAsync(key);
        }

        public override Task SetAsync(string key, T item)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(key));
            return base.SetAsync(key, item);
        }
    }
}