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

// <copyright file="ClientMappers.cs" company="Rolosoft Ltd">
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
    /// The Client Mappers
    /// </summary>
    internal sealed class ClientMappers : BaseMapper<SimpleClient, Client>
    {
        /// <summary>
        /// The claims mapper
        /// </summary>
        private readonly IMapper<SimpleClaim, Claim> claimsMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientMappers"/> class.
        /// </summary>
        /// <param name="claimsMapper">The claims mapper.</param>
        public ClientMappers(IMapper<SimpleClaim, Claim> claimsMapper)
        {
            Contract.Requires(claimsMapper != null);

            this.claimsMapper = claimsMapper;
        }

        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="Client"/></returns>
        public override Client ToComplexEntity(SimpleClient source)
        {
            if (source == null)
            {
                return null;
            }

            var claims = this.claimsMapper.ToComplexEntity(source.Claims).ToList();

            return new Client
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

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="SimpleClient"/></returns>
        public override SimpleClient ToSimpleEntity(Client source)
        {
            if (source == null)
            {
                return null;
            }

            var claims = this.claimsMapper.ToSimpleEntity(source.Claims).ToList();

            return new SimpleClient
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