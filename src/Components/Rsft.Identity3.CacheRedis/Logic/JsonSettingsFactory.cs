﻿// Copyright 2016 Rolosoft Ltd
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
        private static Lazy<JsonSerializerSettings> lazySettings;

        /// <summary>
        /// The client mapper
        /// </summary>
        private readonly IClientMapper<Client> clientMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSettingsFactory"/> class.
        /// </summary>
        /// <param name="clientMapper">The client mapper.</param>
        public JsonSettingsFactory(IClientMapper<Client> clientMapper)
        {
            Contract.Requires(clientMapper != null);

            this.clientMapper = clientMapper;
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
            var settings = new JsonSerializerSettings();

            var scopeMapper = new ScopeMappers();
            var claimMapper = new ClaimMappers();
            var claimsIdentityMapper = new ClaimsIdentityMappers(claimMapper);
            var claimsPrincipalMapper = new ClaimsPrincipalMappers(claimsIdentityMapper);

            settings.Converters.Add(new GenericConverter<SimpleClaim, Claim>(claimMapper));
            settings.Converters.Add(new GenericConverter<SimpleClaimsIdentity, ClaimsIdentity>(claimsIdentityMapper));
            settings.Converters.Add(new GenericConverter<SimpleClaimsPrincipal, ClaimsPrincipal>(claimsPrincipalMapper));
            settings.Converters.Add(new GenericConverter<SimpleScope, Scope>(scopeMapper));

            return settings;
        }
    }
}