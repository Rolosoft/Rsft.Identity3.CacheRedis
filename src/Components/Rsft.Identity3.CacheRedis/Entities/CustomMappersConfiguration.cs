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

// <copyright file="CustomMappersConfiguration.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Entities
{
    using IdentityServer3.Core.Models;
    using Interfaces;
    using Serialization;

    /// <summary>
    /// The Custom Mappers Configuration
    /// </summary>
    public sealed class CustomMappersConfiguration
    {
        /// <summary>
        /// Gets or sets the authorization mapper.
        /// </summary>
        public IMapper<SimpleAuthorizationCode, AuthorizationCode> AuthorizationMapper { get; set; }

        /// <summary>
        /// Gets or sets the client mapper.
        /// </summary>
        public IMapper<SimpleClient, Client> ClientMapper { get; set; }

        /// <summary>
        /// Gets or sets the refresh token mapper.
        /// </summary>
        public IMapper<SimpleRefreshToken, RefreshToken> RefreshTokenMapper { get; set; }

        /// <summary>
        /// Gets or sets the scope mapper.
        /// </summary>
        public IMapper<SimpleScope, Scope> ScopeMapper { get; set; }

        /// <summary>
        /// Gets or sets the token mapper.
        /// </summary>
        public IMapper<SimpleToken, Token> TokenMapper { get; set; }
    }
}