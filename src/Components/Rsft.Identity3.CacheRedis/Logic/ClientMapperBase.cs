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

// <copyright file="ClientMapperBase.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces;

    /// <summary>
    /// The Client Mapper Base
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <seealso cref="IClientMapper{T}" />
    public class ClientMapperBase<T> : IClientMapper<T>
        where T : Client, new()
    {
        /// <summary>
        /// Maps the specified cached client.
        /// </summary>
        /// <param name="cachedClient">The cached client.</param>
        /// <returns>
        /// The TType
        /// </returns>
        public virtual T Map(SimpleClient cachedClient)
        {
            return new T
            {
                Claims = ToComplexEntity(cachedClient.Claims).ToList(),
                ClientId = cachedClient.ClientName,
                Enabled = cachedClient.Enabled,
                AllowAccessToAllCustomGrantTypes = cachedClient.AllowAccessToAllCustomGrantTypes,
                AbsoluteRefreshTokenLifetime = cachedClient.AbsoluteRefreshTokenLifetime,
                AccessTokenLifetime = cachedClient.AccessTokenLifetime,
                AccessTokenType = cachedClient.AccessTokenType,
                ClientName = cachedClient.ClientName,
                AllowAccessToAllScopes = cachedClient.AllowAccessToAllScopes,
                RefreshTokenExpiration = cachedClient.RefreshTokenExpiration,
                AllowedCustomGrantTypes = cachedClient.AllowedCustomGrantTypes,
                AllowedScopes = cachedClient.AllowedScopes,
                EnableLocalLogin = cachedClient.EnableLocalLogin,
                AllowClientCredentialsOnly = cachedClient.AllowClientCredentialsOnly,
                AllowRememberConsent = cachedClient.AllowRememberConsent,
                AllowedCorsOrigins = cachedClient.AllowedCorsOrigins,
                IncludeJwtId = cachedClient.IncludeJwtId,
                UpdateAccessTokenClaimsOnRefresh = cachedClient.UpdateAccessTokenClaimsOnRefresh,
                AlwaysSendClientClaims = cachedClient.AlwaysSendClientClaims,
                ClientSecrets = cachedClient.ClientSecrets,
                ClientUri = cachedClient.ClientUri,
                LogoutSessionRequired = cachedClient.LogoutSessionRequired,
                SlidingRefreshTokenLifetime = cachedClient.SlidingRefreshTokenLifetime,
                IdentityProviderRestrictions = cachedClient.IdentityProviderRestrictions,
                RedirectUris = cachedClient.RedirectUris,
                RequireSignOutPrompt = cachedClient.RequireSignOutPrompt,
                RequireConsent = cachedClient.RequireConsent,
                RefreshTokenUsage = cachedClient.RefreshTokenUsage,
                LogoutUri = cachedClient.LogoutUri,
                PostLogoutRedirectUris = cachedClient.PostLogoutRedirectUris,
                AllowAccessTokensViaBrowser = cachedClient.AllowAccessTokensViaBrowser,
                PrefixClientClaims = cachedClient.PrefixClientClaims,
                AuthorizationCodeLifetime = cachedClient.AuthorizationCodeLifetime,
                IdentityTokenLifetime = cachedClient.IdentityTokenLifetime,
                LogoUri = cachedClient.LogoUri,
                Flow = cachedClient.Flow
            };
        }

        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="claims">The claims.</param>
        /// <returns>The <see cref="Claim"/></returns>
        private static IEnumerable<Claim> ToComplexEntity(IEnumerable<SimpleClaim> claims)
        {
            return claims.Select(r => new Claim(r.Type, r.Value, r.ValueType, r.Issuer, r.OriginalIssuer));
        }
    }
}