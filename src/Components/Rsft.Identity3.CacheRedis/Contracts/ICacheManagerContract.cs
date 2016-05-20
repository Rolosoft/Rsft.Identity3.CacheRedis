// <copyright file="CacheManagerContract.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Contracts
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    using Interfaces;

    [ContractClassFor(typeof(ICacheManager<>))]
    internal abstract class CacheManagerContract<T> : ICacheManager<T>
    {
        /// <summary>
        /// Gets the cache item asynchronously.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<T> GetAsync(string key, CancellationToken cancellationToken)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(key));
            return default(Task<T>);
        }

        /// <summary>
        /// Sets the asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="expiresAtOffset">The expires at offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task SetAsync(string key, T item, DateTimeOffset expiresAtOffset, CancellationToken cancellationToken)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(key));
            return default(Task);
        }
    }
}