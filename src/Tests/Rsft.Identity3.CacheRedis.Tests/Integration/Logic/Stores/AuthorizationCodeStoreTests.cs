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

// <copyright file="AuthorizationCodeStoreTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Tests.Integration.Logic.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using CacheRedis.Logic;
    using CacheRedis.Stores;
    using Entities;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces;
    using Moq;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using TestHelpers;

    /// <summary>
    /// The Authorization CodeStore Tests
    /// </summary>
    /// <seealso cref="TestBase" />
    [TestFixture]
    public sealed class AuthorizationCodeStoreTests : TestBase
    {
        /// <summary>
        /// Gets the asynchronous when called expect response.
        /// </summary>
        [Test]
        public void GetAsync_WhenCalled_ExpectResponse()
        {
            // Arrange
            var mockCacheConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();
            mockCacheConfiguration.Setup(r => r.Get).Returns(
                new RedisCacheConfigurationEntity
                {
                    CacheDuration = 10,
                    RefreshTokenCacheDuration = 10,
                    RedisCacheDefaultPrefix = "DEFAULT",
                    UseObjectCompression = false
                });

            var jsonSettingsFactory = new JsonSettingsFactory();

            var cacheManager = new RedisCacheManager<AuthorizationCode>(
                RedisHelpers.ConnectionMultiplexer,
                mockCacheConfiguration.Object,
                jsonSettingsFactory);

            var authorizationCodeStore = new AuthorizationCodeStore(
                cacheManager,
                mockCacheConfiguration.Object);

            // Act
            var stopwatch = Stopwatch.StartNew();

            var authorizationCode = authorizationCodeStore.GetAsync("Existing").Result;

            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(authorizationCode, Is.Not.Null);

            Assert.That(authorizationCode.Client, Is.Not.Null);
        }

        /// <summary>
        /// Removes the asynchronous when called expect response.
        /// </summary>
        [Test]
        public void RemoveAsync_WhenCalled_ExpectAction()
        {
            // Arrange
            var mockCacheManager = new Mock<ICacheManager<AuthorizationCode>>();
            var mockCacheConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();

            mockCacheConfiguration.Setup(r => r.Get).Returns(new RedisCacheConfigurationEntity { CacheDuration = 1, RefreshTokenCacheDuration = 1 });

            var keyCallback = default(string);
            mockCacheManager.Setup(r => r.DeleteAsync(It.IsAny<string>()))
                .Callback((string s) => keyCallback = s)
                .Returns(Task.FromResult(0));

            var authorizationCodeStore = new AuthorizationCodeStore(
                mockCacheManager.Object,
                mockCacheConfiguration.Object);

            // Act
            var stopwatch = Stopwatch.StartNew();

            authorizationCodeStore.RemoveAsync("string").Wait();

            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(keyCallback, Is.EqualTo("ACS_string"));
            mockCacheManager.Verify(r => r.DeleteAsync(It.IsAny<string>()), Times.Once());
        }

        /// <summary>
        /// Revokes the asynchronous when called expect throws.
        /// </summary>
        [Test]
        public void RevokeAsync_WhenCalled_ExpectThrows()
        {
            // Arrange
            var mockCacheManager = new Mock<ICacheManager<AuthorizationCode>>();
            var mockCacheConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();

            mockCacheConfiguration.Setup(r => r.Get).Returns(new RedisCacheConfigurationEntity { CacheDuration = 1, RefreshTokenCacheDuration = 1 });

            var authorizationCodeStore = new AuthorizationCodeStore(
                mockCacheManager.Object,
                mockCacheConfiguration.Object);

            // Act and Assert
            var stopwatch = Stopwatch.StartNew();

            Assert.Throws<NotImplementedException>(
                () => authorizationCodeStore.RevokeAsync("string", "string").GetAwaiter().GetResult());

            stopwatch.Stop();
            this.WriteTimeElapsed(stopwatch);
        }

        /// <summary>
        /// Stores the asynchronous when called expect action.
        /// </summary>
        [Test]
        public void StoreAsync_WhenCalled_ExpectAction()
        {
            // Arrange
            var cacheConfiguration = new RedisCacheConfigurationDefault
            {
                Get =
                {
                    CacheDuration = 1000,
                    RefreshTokenCacheDuration = 10,
                    RedisCacheDefaultPrefix = "DEFAULT",
                    UseObjectCompression = false
                }
            };

            var jsonSettingsFactory = new JsonSettingsFactory();

            var cacheManager = new RedisCacheManager<AuthorizationCode>(
                RedisHelpers.ConnectionMultiplexer,
                cacheConfiguration,
                jsonSettingsFactory);

            var authorizationCodeStore = new AuthorizationCodeStore(
                cacheManager,
                cacheConfiguration);

            var claim1 = new Claim("claim1", "test@emailippo.com");
            var claim2 = new Claim("claim2", "simon@emailhippo.com");
            var code = new AuthorizationCode
            {
                Client = new Client
                {
                    ClientId = "cid"
                },
                RequestedScopes = new List<Scope> { new Scope { Description = "this is description", Enabled = true, Name = "Scope", DisplayName = "Display Name" } },
                Subject = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { claim1, claim2 }))
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            authorizationCodeStore.StoreAsync("KeyToStore", code).Wait();
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);
        }

        /// <summary>
        /// Tests the fixture setup.
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var database = RedisHelpers.ConnectionMultiplexer.GetDatabase();

            var claim1 = new SimpleClaim { Type = "claim1", Value = "test@emailippo.com" };
            var claim2 = new SimpleClaim { Type = "claim2", Value = "simon@emailhippo.com" };
            var code = new SimpleAuthorizationCode
            {
                Client = new SimpleClient
                {
                    ClientId = "cid"
                },
                RequestedScopes = new List<SimpleScope> { new SimpleScope { Description = "this is description", Enabled = true, Name = "Scope", DisplayName = "Display Name" } },
                Subject = new SimpleClaimsPrincipal
                {
                    Claims = new List<SimpleClaim> { claim1, claim2 },
                    Identities = new List<SimpleClaimsIdentity> { new SimpleClaimsIdentity { Claims = new List<SimpleClaim>() } }
                },
            };

            var settings = new JsonSettingsFactory().Create(false);

            database.StringSet("DEFAULT_ACS_Existing", JsonConvert.SerializeObject(code, settings));
        }

        /// <summary>
        /// Tests the fixture tear down.
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            var database = RedisHelpers.ConnectionMultiplexer.GetDatabase();

            database.KeyDelete("DEFAULT_ACS_Existing");
            database.KeyDelete("DEFAULT_ACS_KeyToStore");
        }
    }
}