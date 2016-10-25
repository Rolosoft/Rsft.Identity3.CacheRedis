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

// <copyright file="AuthorizationCodeStore.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Stores
{
    using System.Diagnostics.Contracts;
    using Entities;
    using IdentityServer3.Core.Models;
    using IdentityServer3.Core.Services;
    using Interfaces;

    /// <summary>
    /// The Authorization Code Store
    /// </summary>
    /// <seealso cref="IAuthorizationCodeStore" />
    public sealed class AuthorizationCodeStore : RedisStoreBase<AuthorizationCode>, IAuthorizationCodeStore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationCodeStore"/> class.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="cacheConfiguration">The cache configuration.</param>
        internal AuthorizationCodeStore(
            ICacheManager<AuthorizationCode> cacheManager,
            IConfiguration<RedisCacheConfigurationEntity> cacheConfiguration)
            : base(cacheManager, cacheConfiguration)
        {
            Contract.Requires(cacheManager != null);
            Contract.Requires(cacheConfiguration != null);
        }

        /// <summary>
        /// Prefixes the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The Prefixed Key</returns>
        protected override string PrefixKey(string key)
        {
            return $"ACS_{key}";
        }
    }
}