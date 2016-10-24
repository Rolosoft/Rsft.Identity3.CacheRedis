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

// <copyright file="Caches.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Entities
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using IdentityServer3.Core.Models;
    using IdentityServer3.Core.Services;

    /// <summary>
    /// The caches.
    /// </summary>
    /// <remarks>
    /// <para>Plug these caches into Identity Server 3 on configuration startup.</para>
    /// <para>See Identity Server 3 documetation for further information.</para>
    /// </remarks>
    public sealed class Caches
    {
        /// <summary>
        /// Gets the authorization code store.
        /// </summary>
        /// <value>
        /// The authorization code store.
        /// </value>
        public IAuthorizationCodeStore AuthorizationCodeStore { get; internal set; }

        /// <summary>
        /// Gets the client cache.
        /// </summary>
        /// <value>
        /// The client cache.
        /// </value>
        public ICache<Client> ClientCache { get; internal set; }

        /// <summary>
        /// Gets the refresh token store.
        /// </summary>
        /// <value>
        /// The refresh token store.
        /// </value>
        public IRefreshTokenStore RefreshTokenStore { get; internal set; }

        /// <summary>
        /// Gets the scopes cache.
        /// </summary>
        /// <value>
        /// The scopes cache.
        /// </value>
        public ICache<IEnumerable<Scope>> ScopesCache { get; internal set; }

        /// <summary>
        /// Gets the token handle store.
        /// </summary>
        /// <value>
        /// The token handle store.
        /// </value>
        public ITokenHandleStore TokenHandleStore { get; internal set; }

        /// <summary>
        /// Gets the user service cache.
        /// </summary>
        /// <value>
        /// The user service cache.
        /// </value>
        public ICache<IEnumerable<Claim>> UserServiceCache { get; internal set; }
    }
}