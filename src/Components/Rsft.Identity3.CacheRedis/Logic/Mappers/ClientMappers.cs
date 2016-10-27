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
    using Interfaces.Serialization;

    /// <summary>
    /// The Client Mappers
    /// </summary>
    /// <typeparam name="TClient">The type of the client.</typeparam>
    /// <seealso cref="GenericMapper{SimpleClient, TClient}" />
    /// <seealso cref="SimpleClient" />
    public sealed class ClientMappers<TClient> : GenericMapper<SimpleClient, TClient>
        where TClient : Client, new()
    {
        /// <summary>
        /// The claims mapper
        /// </summary>
        private readonly IMapper<SimpleClaim, Claim> claimsMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientMappers{TClient}"/> class.
        /// </summary>
        /// <param name="claimsMapper">The claims mapper.</param>
        /// <param name="propertyMapper">The property mapper.</param>
        internal ClientMappers(IMapper<SimpleClaim, Claim> claimsMapper, IPropertyGetSettersTyped<TClient> propertyMapper)
            : base(propertyMapper)
        {
            Contract.Requires(claimsMapper != null);
            Contract.Requires(propertyMapper != null);

            this.claimsMapper = claimsMapper;
        }

        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="TClient"/></returns>
        public override TClient ToComplexEntity(SimpleClient source)
        {
            if (source == null)
            {
                return null;
            }

            var client = base.ToComplexEntity(source);

            var claims = this.claimsMapper.ToComplexEntity(source.Claims).ToList();

            client.Claims = claims;
            client.AbsoluteRefreshTokenLifetime = source.AbsoluteRefreshTokenLifetime;
            client.AccessTokenLifetime = source.AccessTokenLifetime;
            client.AccessTokenType = source.AccessTokenType;
            client.AllowAccessToAllCustomGrantTypes = source.AllowAccessToAllCustomGrantTypes;
            client.AllowAccessToAllScopes = source.AllowAccessToAllScopes;
            client.AllowAccessTokensViaBrowser = source.AllowAccessTokensViaBrowser;
            client.AllowClientCredentialsOnly = source.AllowClientCredentialsOnly;
            client.AllowRememberConsent = source.AllowRememberConsent;
            client.AllowedCorsOrigins = source.AllowedCorsOrigins;
            client.AllowedCustomGrantTypes = source.AllowedCustomGrantTypes;
            client.AllowedScopes = source.AllowedScopes;
            client.AlwaysSendClientClaims = source.AlwaysSendClientClaims;
            client.AuthorizationCodeLifetime = source.AuthorizationCodeLifetime;
            client.ClientId = source.ClientId;
            client.ClientName = source.ClientName;
            client.ClientSecrets = source.ClientSecrets;
            client.ClientUri = source.ClientUri;
            client.EnableLocalLogin = source.EnableLocalLogin;
            client.Enabled = source.Enabled;
            client.Flow = source.Flow;
            client.IdentityProviderRestrictions = source.IdentityProviderRestrictions;
            client.IdentityTokenLifetime = source.IdentityTokenLifetime;
            client.IncludeJwtId = source.IncludeJwtId;
            client.LogoUri = source.LogoUri;
            client.LogoutSessionRequired = source.LogoutSessionRequired;
            client.LogoutUri = source.LogoutUri;
            client.PostLogoutRedirectUris = source.PostLogoutRedirectUris;
            client.PrefixClientClaims = source.PrefixClientClaims;
            client.RedirectUris = source.RedirectUris;
            client.RefreshTokenExpiration = source.RefreshTokenExpiration;
            client.RefreshTokenUsage = source.RefreshTokenUsage;
            client.RequireConsent = source.RequireConsent;
            client.RequireSignOutPrompt = source.RequireSignOutPrompt;
            client.SlidingRefreshTokenLifetime = source.SlidingRefreshTokenLifetime;
            client.UpdateAccessTokenClaimsOnRefresh = source.UpdateAccessTokenClaimsOnRefresh;

            return client;
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="SimpleClient"/></returns>
        public override SimpleClient ToSimpleEntity(object source)
        {
            if (source == null)
            {
                return null;
            }

            var clientSource = (TClient)source;

            var client = base.ToSimpleEntity(clientSource);

            var claims = this.claimsMapper.ToSimpleEntity(clientSource.Claims).ToList();

            client.Claims = claims;
            client.AbsoluteRefreshTokenLifetime = clientSource.AbsoluteRefreshTokenLifetime;
            client.AccessTokenLifetime = clientSource.AccessTokenLifetime;
            client.AccessTokenType = clientSource.AccessTokenType;
            client.AllowAccessToAllCustomGrantTypes = clientSource.AllowAccessToAllCustomGrantTypes;
            client.AllowAccessToAllScopes = clientSource.AllowAccessToAllScopes;
            client.AllowAccessTokensViaBrowser = clientSource.AllowAccessTokensViaBrowser;
            client.AllowClientCredentialsOnly = clientSource.AllowClientCredentialsOnly;
            client.AllowRememberConsent = clientSource.AllowRememberConsent;
            client.AllowedCorsOrigins = clientSource.AllowedCorsOrigins;
            client.AllowedCustomGrantTypes = clientSource.AllowedCustomGrantTypes;
            client.AllowedScopes = clientSource.AllowedScopes;
            client.AlwaysSendClientClaims = clientSource.AlwaysSendClientClaims;
            client.AuthorizationCodeLifetime = clientSource.AuthorizationCodeLifetime;
            client.ClientId = clientSource.ClientId;
            client.ClientName = clientSource.ClientName;
            client.ClientSecrets = clientSource.ClientSecrets;
            client.ClientUri = clientSource.ClientUri;
            client.EnableLocalLogin = clientSource.EnableLocalLogin;
            client.Enabled = clientSource.Enabled;
            client.Flow = clientSource.Flow;
            client.IdentityProviderRestrictions = clientSource.IdentityProviderRestrictions;
            client.IdentityTokenLifetime = clientSource.IdentityTokenLifetime;
            client.IncludeJwtId = clientSource.IncludeJwtId;
            client.LogoUri = clientSource.LogoUri;
            client.LogoutSessionRequired = clientSource.LogoutSessionRequired;
            client.LogoutUri = clientSource.LogoutUri;
            client.PostLogoutRedirectUris = clientSource.PostLogoutRedirectUris;
            client.PrefixClientClaims = clientSource.PrefixClientClaims;
            client.RedirectUris = clientSource.RedirectUris;
            client.RefreshTokenExpiration = clientSource.RefreshTokenExpiration;
            client.RefreshTokenUsage = clientSource.RefreshTokenUsage;
            client.RequireConsent = clientSource.RequireConsent;
            client.RequireSignOutPrompt = clientSource.RequireSignOutPrompt;
            client.SlidingRefreshTokenLifetime = clientSource.SlidingRefreshTokenLifetime;
            client.UpdateAccessTokenClaimsOnRefresh = clientSource.UpdateAccessTokenClaimsOnRefresh;

            return client;
        }
    }
}