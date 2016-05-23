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