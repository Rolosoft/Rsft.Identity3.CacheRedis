// <copyright file="ICacheManager.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Interfaces
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    using Contracts;

    [ContractClass(typeof(CacheManagerContract<>))]
    internal interface ICacheManager<T>
    {
        /// <summary>
        /// Gets the cache item asynchronously.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> GetAsync(string key);

        /// <summary>
        /// Sets the asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="timeSpan">The time span.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        Task SetAsync(string key, T item, TimeSpan timeSpan);
    }
}