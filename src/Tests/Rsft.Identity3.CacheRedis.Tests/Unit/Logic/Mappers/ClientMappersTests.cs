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

// <copyright file="ClientMappersTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Unit.Logic.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Security.Claims;
    using CacheRedis.Logic.Mappers;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The Client Mappers Tests
    /// </summary>
    /// <seealso cref="Rsft.Identity3.CacheRedis.Tests.TestBase" />
    [TestFixture]
    public sealed class ClientMappersTests : TestBase
    {
        /// <summary>
        /// To the complex entity when simple entity expect map success.
        /// </summary>
        [Test]
        public void ToComplexEntity_WhenSimpleEntity_ExpectMapSuccess()
        {
            // Arrange
            var mockClaimsMapper = new Mock<IMapper<SimpleClaim, Claim>>();

            mockClaimsMapper.Setup(r => r.ToComplexEntity(It.IsAny<IEnumerable<SimpleClaim>>())).Returns(new List<Claim> { new Claim("DEFAULT", "DEFAULT") });

            var clientMappers = new ClientMappers(mockClaimsMapper.Object);

            var secret = new Secret("Value", "Description", new DateTimeOffset(new DateTime(2016, 1, 1))) { Type = "Type" };

            var simpleClient = new SimpleClient
            {
                Claims = new List<SimpleClaim>(),
                Enabled = true,
                AccessTokenType = AccessTokenType.Jwt,
                AbsoluteRefreshTokenLifetime = 1,
                AccessTokenLifetime = 1,
                AllowAccessToAllCustomGrantTypes = true,
                AllowAccessToAllScopes = true,
                AllowRememberConsent = true,
                EnableLocalLogin = true,
                AllowAccessTokensViaBrowser = true,
                LogoutSessionRequired = true,
                Flow = Flows.AuthorizationCode,
                AlwaysSendClientClaims = true,
                PrefixClientClaims = true,
                ClientSecrets = new List<Secret> { secret },
                RefreshTokenExpiration = TokenExpiration.Absolute,
                RequireSignOutPrompt = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                IdentityTokenLifetime = 1,
                SlidingRefreshTokenLifetime = 1,
                RequireConsent = true,
                AllowClientCredentialsOnly = true,
                IncludeJwtId = true,
                AuthorizationCodeLifetime = 1,
                UpdateAccessTokenClaimsOnRefresh = true,
                ClientName = "ClientName",
                LogoutUri = "LogoutUri",
                RedirectUris = new List<string>(),
                ClientUri = "ClientUri",
                AllowedCustomGrantTypes = new List<string>(),
                AllowedScopes = new List<string>(),
                ClientId = "ClientId",
                PostLogoutRedirectUris = new List<string>(),
                AllowedCorsOrigins = new List<string>(),
                IdentityProviderRestrictions = new List<string>(),
                LogoUri = "LogoUri"
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var complexEntity = clientMappers.ToComplexEntity(simpleClient);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(complexEntity, Is.Not.Null);
        }

        /// <summary>
        /// To the simple entity when complex entity expect map success.
        /// </summary>
        [Test]
        public void ToSimpleEntity_WhenComplexEntity_ExpectMapSuccess()
        {
            // Arrange
            var mockClaimsMapper = new Mock<IMapper<SimpleClaim, Claim>>();

            mockClaimsMapper.Setup(r => r.ToSimpleEntity(It.IsAny<IEnumerable<Claim>>())).Returns(new List<SimpleClaim> { new SimpleClaim() });

            var clientMappers = new ClientMappers(mockClaimsMapper.Object);

            var secret = new Secret("Value", "Description", new DateTimeOffset(new DateTime(2016, 1, 1))) { Type = "Type" };

            var client = new Client
            {
                Claims = new List<Claim>(),
                Enabled = true,
                AccessTokenType = AccessTokenType.Jwt,
                AbsoluteRefreshTokenLifetime = 1,
                AccessTokenLifetime = 1,
                AllowAccessToAllCustomGrantTypes = true,
                AllowAccessToAllScopes = true,
                AllowRememberConsent = true,
                EnableLocalLogin = true,
                AllowAccessTokensViaBrowser = true,
                LogoutSessionRequired = true,
                Flow = Flows.AuthorizationCode,
                AlwaysSendClientClaims = true,
                PrefixClientClaims = true,
                ClientSecrets = new List<Secret> { secret },
                RefreshTokenExpiration = TokenExpiration.Absolute,
                RequireSignOutPrompt = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                IdentityTokenLifetime = 1,
                SlidingRefreshTokenLifetime = 1,
                RequireConsent = true,
                AllowClientCredentialsOnly = true,
                IncludeJwtId = true,
                AuthorizationCodeLifetime = 1,
                UpdateAccessTokenClaimsOnRefresh = true,
                ClientName = "ClientName",
                LogoutUri = "LogoutUri",
                RedirectUris = new List<string>(),
                ClientUri = "ClientUri",
                AllowedCustomGrantTypes = new List<string>(),
                AllowedScopes = new List<string>(),
                ClientId = "ClientId",
                PostLogoutRedirectUris = new List<string>(),
                AllowedCorsOrigins = new List<string>(),
                IdentityProviderRestrictions = new List<string>(),
                LogoUri = "LogoUri"
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var simpleEntity = clientMappers.ToSimpleEntity(client);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(simpleEntity, Is.Not.Null);
        }
    }
}