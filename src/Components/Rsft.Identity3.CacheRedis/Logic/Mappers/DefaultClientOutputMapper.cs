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

// <copyright file="DefaultClientOutputMapper.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Mappers
{
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Security.Claims;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces;

    /// <summary>
    /// The Default Client Mapper
    /// </summary>
    /// <typeparam name="TClient">The type of the client.</typeparam>
    /// <seealso cref="IConfigMapper{SimpleClient, TClient}" />
    public class DefaultClientOutputMapper<TClient> : IOutputMapper<SimpleClient, TClient>
        where TClient : Client, new()
    {
        /// <summary>
        /// The claims mapper
        /// </summary>
        private readonly IOutputMapper<SimpleClaim, Claim> claimsMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultClientOutputMapper{TClient}"/> class.
        /// </summary>
        /// <param name="claimsMapper">The claims mapper.</param>
        protected DefaultClientOutputMapper(IOutputMapper<SimpleClaim, Claim> claimsMapper)
        {
            Contract.Requires(claimsMapper != null);

            this.claimsMapper = claimsMapper;
        }

        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The TClient</returns>
        public virtual TClient Map(SimpleClient source)
        {
            if (source == null)
            {
                return null;
            }

            var claims = source.Claims.Select(r => this.claimsMapper.Map(r)).ToList();

            return new TClient
            {
                Claims = claims,
                AbsoluteRefreshTokenLifetime = source.AbsoluteRefreshTokenLifetime,
                AccessTokenLifetime = source.AccessTokenLifetime,
                AccessTokenType = source.AccessTokenType,
                AllowAccessToAllCustomGrantTypes = source.AllowAccessToAllCustomGrantTypes,
                AllowAccessToAllScopes = source.AllowAccessToAllScopes,
                AllowAccessTokensViaBrowser = source.AllowAccessTokensViaBrowser,
                AllowClientCredentialsOnly = source.AllowClientCredentialsOnly,
                AllowRememberConsent = source.AllowRememberConsent,
                AllowedCorsOrigins = source.AllowedCorsOrigins,
                AllowedCustomGrantTypes = source.AllowedCustomGrantTypes,
                AllowedScopes = source.AllowedScopes,
                AlwaysSendClientClaims = source.AlwaysSendClientClaims,
                AuthorizationCodeLifetime = source.AuthorizationCodeLifetime,
                ClientId = source.ClientId,
                ClientName = source.ClientName,
                ClientSecrets = source.ClientSecrets,
                ClientUri = source.ClientUri,
                EnableLocalLogin = source.EnableLocalLogin,
                Enabled = source.Enabled,
                Flow = source.Flow,
                IdentityProviderRestrictions = source.IdentityProviderRestrictions,
                IdentityTokenLifetime = source.IdentityTokenLifetime,
                IncludeJwtId = source.IncludeJwtId,
                LogoUri = source.LogoUri,
                LogoutSessionRequired = source.LogoutSessionRequired,
                LogoutUri = source.LogoutUri,
                PostLogoutRedirectUris = source.PostLogoutRedirectUris,
                PrefixClientClaims = source.PrefixClientClaims,
                RedirectUris = source.RedirectUris,
                RefreshTokenExpiration = source.RefreshTokenExpiration,
                RefreshTokenUsage = source.RefreshTokenUsage,
                RequireConsent = source.RequireConsent,
                RequireSignOutPrompt = source.RequireSignOutPrompt,
                SlidingRefreshTokenLifetime = source.SlidingRefreshTokenLifetime,
                UpdateAccessTokenClaimsOnRefresh = source.UpdateAccessTokenClaimsOnRefresh
            };
        }
    }
}