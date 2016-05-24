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

// <copyright file="ICacheManager.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    using Contracts;

    /// <summary>
    /// The cache manager interface.
    /// </summary>
    /// <typeparam name="T">Type of object to manage.</typeparam>
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
        /// Gets all the cache items specified by keys asynchronously.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IDictionary<string, T>> GetAllAsync(IEnumerable<string> keys);

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