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

// <copyright file="JsonSettingsFactory.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Security.Claims;
    using Entities;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces;
    using Newtonsoft.Json;
    using Serialization;

    /// <summary>
    /// The JSON Settings Factory
    /// </summary>
    /// <seealso cref="Rsft.Identity3.CacheRedis.Interfaces.IJsonSettingsFactory" />
    public sealed class JsonSettingsFactory : IJsonSettingsFactory
    {
        /// <summary>
        /// The lazy settings
        /// </summary>
        private static Lazy<JsonSerializerSettings> lazySettings;

        /// <summary>
        /// The custom mappers configuration
        /// </summary>
        private readonly CustomMappersConfiguration customMappersConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSettingsFactory" /> class.
        /// </summary>
        /// <param name="customMappersConfiguration">The custom mappers configuration.</param>
        public JsonSettingsFactory(CustomMappersConfiguration customMappersConfiguration)
        {
            Contract.Requires(customMappersConfiguration != null);

            this.customMappersConfiguration = customMappersConfiguration;
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonSerializerSettings" />
        /// </returns>
        public JsonSerializerSettings Create()
        {
            if (lazySettings == null)
            {
                lazySettings = new Lazy<JsonSerializerSettings>(this.Initialize);
            }

            return lazySettings.Value;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonSerializerSettings" />
        /// </returns>
        private JsonSerializerSettings Initialize()
        {
            var clientMapper = this.customMappersConfiguration.ClientMapper ??
                               CustomMapperFactory.CreateClientMapper<Client>();

            var scopeMapper = this.customMappersConfiguration.ScopeMapper ??
                              CustomMapperFactory.CreateScopeMapper<Scope>();

            var authorizationMapper = this.customMappersConfiguration.AuthorizationMapper ??
                                      CustomMapperFactory.CreateAuthorizationCodeMapper<AuthorizationCode>(clientMapper, scopeMapper);

            var tokenMapper = this.customMappersConfiguration.TokenMapper ??
                              CustomMapperFactory.CreateTokenMapper<Token>(clientMapper);

            var refreshTokenMapper = this.customMappersConfiguration.RefreshTokenMapper ??
                                     CustomMapperFactory.CreateRefreshTokenMapper<RefreshToken>(tokenMapper);

            var settings = new JsonSerializerSettings();

            var claimMapper = CustomMapperFactory.ClaimMapper;
            var claimsIdentityMapper = CustomMapperFactory.ClaimsIdentityMapper;
            var claimsPrincipalMapper = CustomMapperFactory.ClaimsPrincipalMapper;

            settings.Converters.Add(new GenericConverter<SimpleAuthorizationCode, AuthorizationCode>(authorizationMapper));
            settings.Converters.Add(new GenericConverter<SimpleClaim, Claim>(claimMapper));
            settings.Converters.Add(new GenericConverter<SimpleClaimsIdentity, ClaimsIdentity>(claimsIdentityMapper));
            settings.Converters.Add(new GenericConverter<SimpleClaimsPrincipal, ClaimsPrincipal>(claimsPrincipalMapper));
            settings.Converters.Add(new GenericConverter<SimpleClient, Client>(clientMapper));
            settings.Converters.Add(new GenericConverter<SimpleRefreshToken, RefreshToken>(refreshTokenMapper));
            settings.Converters.Add(new GenericConverter<SimpleScope, Scope>(scopeMapper));
            settings.Converters.Add(new GenericConverter<SimpleToken, Token>(tokenMapper));

            return settings;
        }
    }
}