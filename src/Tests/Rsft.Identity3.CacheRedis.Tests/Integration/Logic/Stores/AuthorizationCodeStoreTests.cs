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
    using System.Linq;
    using System.Security.Claims;
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
    [Ignore("Set REDIS Connection string in TestHelpers.RedisHelpers to your local dev store")]
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
            Assert.That(authorizationCode.ClientId, Is.EqualTo("cid"));
            Assert.That(authorizationCode.CodeChallenge, Is.EqualTo("CodeChallenge"));
            Assert.That(authorizationCode.CodeChallengeMethod, Is.EqualTo("CodeChallengeMethod"));
            Assert.That(authorizationCode.CreationTime, Is.EqualTo(new DateTimeOffset(new DateTime(2016, 1, 1))));
            Assert.That(authorizationCode.IsOpenId, Is.True);
            Assert.That(authorizationCode.Nonce, Is.EqualTo("Nonce"));
            Assert.That(authorizationCode.RedirectUri, Is.EqualTo("RedirectUri"));

            Assert.That(authorizationCode.RequestedScopes, Is.Not.Null);
            Assert.That(authorizationCode.RequestedScopes.Count(), Is.EqualTo(1));

            Assert.That(authorizationCode.Scopes, Is.Not.Null);
            Assert.That(authorizationCode.Scopes.Count(), Is.EqualTo(1));

            Assert.That(authorizationCode.SessionId, Is.EqualTo("SessionId"));
            Assert.That(authorizationCode.WasConsentShown, Is.True);
            Assert.That(authorizationCode.Subject, Is.Not.Null);
        }

        /// <summary>
        /// Removes the asynchronous when called expect response.
        /// </summary>
        [Test]
        public void RemoveAsync_WhenCalled_ExpectAction()
        {
            // Arrange
            var mockCacheConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();
            mockCacheConfiguration.Setup(r => r.Get).Returns(
                new RedisCacheConfigurationEntity
                {
                    CacheDuration = 10,
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

            authorizationCodeStore.RemoveAsync("Delete").Wait();

            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            var redisValue = RedisHelpers.ConnectionMultiplexer.GetDatabase().StringGet("DEFAULT_ACS_Delete");

            Assert.That(redisValue.HasValue, Is.False);
        }

        /// <summary>
        /// Stores the asynchronous when called expect action.
        /// </summary>
        [Test]
        public void StoreAsync_WhenCalled_ExpectAction()
        {
            // Arrange
            var mockCacheConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();
            mockCacheConfiguration.Setup(r => r.Get).Returns(
                new RedisCacheConfigurationEntity
                {
                    CacheDuration = 10,
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

            var redisValue = RedisHelpers.ConnectionMultiplexer.GetDatabase().StringGet("DEFAULT_ACS_KeyToStore");

            Assert.That(redisValue.HasValue, Is.True);
            Console.WriteLine(redisValue);
        }

        /// <summary>
        /// Tests the fixture setup.
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var database = RedisHelpers.ConnectionMultiplexer.GetDatabase();

            var code = new SimpleAuthorizationCode
            {
                Client = new SimpleClient
                {
                    ClientId = "cid"
                },
                RequestedScopes =
                    new List<SimpleScope>
                    {
                        new SimpleScope
                        {
                            Description = "this is description",
                            Enabled = true,
                            Name = "Scope",
                            DisplayName = "Display Name"
                        }
                    },
                Subject = new SimpleClaimsPrincipal
                {
                    Identities = new List<SimpleClaimsIdentity> { new SimpleClaimsIdentity { Claims = new List<SimpleClaim>() } }
                },
                CodeChallenge = "CodeChallenge",
                CodeChallengeMethod = "CodeChallengeMethod",
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                IsOpenId = true,
                Nonce = "Nonce",
                RedirectUri = "RedirectUri",
                SessionId = "SessionId",
                WasConsentShown = true
            };

            var settings = new JsonSettingsFactory().Create(false);

            var serialized = JsonConvert.SerializeObject(code, settings);

            database.StringSet("DEFAULT_ACS_Existing", serialized);
            database.StringSet("DEFAULT_ACS_Delete", serialized);
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
            database.KeyDelete("DEFAULT_ACS_Delete");
        }
    }
}