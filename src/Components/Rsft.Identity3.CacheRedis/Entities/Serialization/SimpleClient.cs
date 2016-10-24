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

// <copyright file="SimpleClient.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Entities.Serialization
{
    using System.Collections.Generic;
    using IdentityServer3.Core.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// The Simple Client
    /// </summary>
    internal sealed class SimpleClient
    {
        /// <summary>
        /// Gets or sets the absolute refresh token lifetime.
        /// </summary>
        [JsonProperty("artl")]
        public int AbsoluteRefreshTokenLifetime { get; set; }

        /// <summary>
        /// Gets or sets the access token lifetime.
        /// </summary>
        [JsonProperty("atl")]
        public int AccessTokenLifetime { get; set; }

        /// <summary>
        /// Gets or sets the type of the access token.
        /// </summary>
        [JsonProperty("att")]
        public AccessTokenType AccessTokenType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow access to all custom grant types].
        /// </summary>
        [JsonProperty("aatacgt")]
        public bool AllowAccessToAllCustomGrantTypes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow access to all scopes].
        /// </summary>
        [JsonProperty("aatas")]
        public bool AllowAccessToAllScopes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow access tokens via browser].
        /// </summary>
        [JsonProperty("aatvb")]
        public bool AllowAccessTokensViaBrowser { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow client credentials only].
        /// </summary>
        [JsonProperty("acco")]
        public bool AllowClientCredentialsOnly { get; set; }

        /// <summary>
        /// Gets or sets the allowed cors origins.
        /// </summary>
        [JsonProperty("aco")]
        public List<string> AllowedCorsOrigins { get; set; }

        /// <summary>
        /// Gets or sets the allowed custom grant types.
        /// </summary>
        [JsonProperty("acgt")]
        public List<string> AllowedCustomGrantTypes { get; set; }

        /// <summary>
        /// Gets or sets the allowed scopes.
        /// </summary>
        [JsonProperty("as")]
        public List<string> AllowedScopes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow remember consent].
        /// </summary>
        [JsonProperty("arc")]
        public bool AllowRememberConsent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [always send client claims].
        /// </summary>
        [JsonProperty("ascc")]
        public bool AlwaysSendClientClaims { get; set; }

        /// <summary>
        /// Gets or sets the authorization code lifetime.
        /// </summary>
        [JsonProperty("acl")]
        public int AuthorizationCodeLifetime { get; set; }

        /// <summary>
        /// Gets or sets the claims.
        /// </summary>
        [JsonProperty("c")]
        public List<SimpleClaim> Claims { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        [JsonProperty("ci")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        [JsonProperty("cn")]
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the client secrets.
        /// </summary>
        [JsonProperty("cs")]
        public List<Secret> ClientSecrets { get; set; }

        /// <summary>
        /// Gets or sets the client URI.
        /// </summary>
        [JsonProperty("cu")]
        public string ClientUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SimpleClient"/> is enabled.
        /// </summary>
        [JsonProperty("e")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable local login].
        /// </summary>
        [JsonProperty("ell")]
        public bool EnableLocalLogin { get; set; }

        /// <summary>
        /// Gets or sets the flow.
        /// </summary>#
        [JsonProperty("f")]
        public Flows Flow { get; set; }

        /// <summary>
        /// Gets or sets the identity provider restrictions.
        /// </summary>
        [JsonProperty("apr")]
        public List<string> IdentityProviderRestrictions { get; set; }

        /// <summary>
        /// Gets or sets the identity token lifetime.
        /// </summary>
        [JsonProperty("itl")]
        public int IdentityTokenLifetime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include JWT identifier].
        /// </summary>
        [JsonProperty("iji")]
        public bool IncludeJwtId { get; set; }

        /// <summary>
        /// Gets or sets the logo URI.
        /// </summary>
        [JsonProperty("lu")]
        public string LogoUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [logout session required].
        /// </summary>
        [JsonProperty("lsr")]
        public bool LogoutSessionRequired { get; set; }

        /// <summary>
        /// Gets or sets the logout URI.
        /// </summary>
        [JsonProperty("lou")]
        public string LogoutUri { get; set; }

        /// <summary>
        /// Gets or sets the post logout redirect uris.
        /// </summary>
        [JsonProperty("plru")]
        public List<string> PostLogoutRedirectUris { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [prefix client claims].
        /// </summary>
        [JsonProperty("pcc")]
        public bool PrefixClientClaims { get; set; }

        /// <summary>
        /// Gets or sets the redirect uris.
        /// </summary>
        [JsonProperty("ru")]
        public List<string> RedirectUris { get; set; }

        /// <summary>
        /// Gets or sets the refresh token expiration.
        /// </summary>
        [JsonProperty("rte")]
        public TokenExpiration RefreshTokenExpiration { get; set; }

        /// <summary>
        /// Gets or sets the refresh token usage.
        /// </summary>
        [JsonProperty("rtu")]
        public TokenUsage RefreshTokenUsage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [require consent].
        /// </summary>
        [JsonProperty("rc")]
        public bool RequireConsent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [require sign out prompt].
        /// </summary>
        [JsonProperty("rsop")]
        public bool RequireSignOutPrompt { get; set; }

        /// <summary>
        /// Gets or sets the sliding refresh token lifetime.
        /// </summary>
        [JsonProperty("srtl")]
        public int SlidingRefreshTokenLifetime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [update access token claims on refresh].
        /// </summary>
        [JsonProperty("uatcor")]
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
    }
}