// <copyright file="CacheManagerContract.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Contracts
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    using Interfaces;

    [ContractClassFor(typeof(ICacheManager<>))]
    internal abstract class CacheManagerContract<T> : ICacheManager<T>
    {
        /// <summary>
        /// Gets the cache item asynchronously.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<T> GetAsync(string key)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(key));
            return default(Task<T>);
        }

        /// <summary>
        /// Sets the asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="timeSpan">The time span.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public Task SetAsync(string key, T item, TimeSpan timeSpan)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(key));
            return default(Task);
        }
    }
}