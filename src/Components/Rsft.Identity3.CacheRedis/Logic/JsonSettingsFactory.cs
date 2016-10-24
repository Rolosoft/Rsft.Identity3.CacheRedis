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
    using System.Security.Claims;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces;
    using Mappers;
    using Newtonsoft.Json;
    using Serialization;

    public sealed class JsonSettingsFactory : IJsonSettingsFactory
    {
        /// <summary>
        /// The lazy settings
        /// </summary>
        private static readonly Lazy<JsonSerializerSettings> LazySettings = new Lazy<JsonSerializerSettings>(Initialize);

        /// <summary>
        /// The cache configuration
        /// </summary>
        private static bool useCompressionLocal;

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <param name="useCompression">if set to <c>true</c> [use compression].</param>
        /// <returns>
        /// The <see cref="JsonSerializerSettings" />
        /// </returns>
        public JsonSerializerSettings Create(bool useCompression)
        {
            useCompressionLocal = useCompression;

            return LazySettings.Value;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonSerializerSettings" />
        /// </returns>
        private static JsonSerializerSettings Initialize()
        {
            var settings = new JsonSerializerSettings();

            if (!useCompressionLocal)
            {
                settings.ContractResolver = new JsonUseLongNames();
            }

            var scopeMapper = new ScopeMappers();
            var claimMapper = new ClaimMappers();
            var clientMapper = new ClientMappers(claimMapper);
            var claimsIdentityMapper = new ClaimsIdentityMappers(claimMapper);
            var claimsPrincipalMapper = new ClaimsPrincipalMappers(claimsIdentityMapper);
            var authorizationCodeMapper = new AuthorizationCodeMappers(claimsPrincipalMapper, clientMapper, scopeMapper);

            settings.Converters.Add(new GenericConverter<SimpleAuthorizationCode, AuthorizationCode>(authorizationCodeMapper));
            settings.Converters.Add(new GenericConverter<SimpleClaim, Claim>(claimMapper));
            settings.Converters.Add(new GenericConverter<SimpleClaimsIdentity, ClaimsIdentity>(claimsIdentityMapper));
            settings.Converters.Add(new GenericConverter<SimpleClaimsPrincipal, ClaimsPrincipal>(claimsPrincipalMapper));
            settings.Converters.Add(new GenericConverter<SimpleClient, Client>(clientMapper));
            settings.Converters.Add(new GenericConverter<SimpleScope, Scope>(scopeMapper));

            return settings;
        }
    }
}