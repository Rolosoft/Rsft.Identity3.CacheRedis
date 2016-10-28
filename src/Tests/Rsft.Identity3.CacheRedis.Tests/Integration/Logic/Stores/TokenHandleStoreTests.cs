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

// <copyright file="TokenHandleStoreTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Integration.Logic.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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
    /// The Token Handle Store Tests
    /// </summary>
    [TestFixture]
    [Ignore("Set REDIS Connection string in TestHelpers.RedisHelpers to your local dev store")]
    public sealed class TokenHandleStoreTests : TestBase
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

            var jsonSettingsFactory = new JsonSettingsFactory(new CustomMappersConfiguration());

            var cacheManager = new RedisCacheManager<Token>(
                RedisHelpers.ConnectionMultiplexer,
                mockCacheConfiguration.Object,
                jsonSettingsFactory.Create());

            var tokenStore = new TokenHandleStore(
                cacheManager,
                mockCacheConfiguration.Object);

            // Act
            var stopwatch = Stopwatch.StartNew();
            var token = tokenStore.GetAsync("Existing").Result;
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(token, Is.Not.Null);
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

            var jsonSettingsFactory = new JsonSettingsFactory(new CustomMappersConfiguration());

            var cacheManager = new RedisCacheManager<Token>(
                RedisHelpers.ConnectionMultiplexer,
                mockCacheConfiguration.Object,
                jsonSettingsFactory.Create());

            var tokenStore = new TokenHandleStore(
                cacheManager,
                mockCacheConfiguration.Object);

            // Act
            var stopwatch = Stopwatch.StartNew();

            tokenStore.RemoveAsync("Delete").Wait();

            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            var redisValue = RedisHelpers.ConnectionMultiplexer.GetDatabase().StringGet("DEFAULT_RTS_Delete");

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

            var jsonSettingsFactory = new JsonSettingsFactory(new CustomMappersConfiguration());

            var cacheManager = new RedisCacheManager<Token>(
                RedisHelpers.ConnectionMultiplexer,
                mockCacheConfiguration.Object,
                jsonSettingsFactory.Create());

            var tokenStore = new TokenHandleStore(
                cacheManager,
                mockCacheConfiguration.Object);

            var claim1 = new Claim("Type1", "Value1");
            var claim2 = new Claim("Type2", "Value2");

            var client = new Client
            {
                Claims = new List<Claim> { claim1, claim2 }
            };

            var token = new Token
            {
                Claims = new List<Claim> { claim1, claim2 },
                Client = client,
                Type = "Type",
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                Version = 1,
                Issuer = "Issuer",
                Lifetime = 120,
                Audience = "Audience"
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            tokenStore.StoreAsync("KeyToStore", token).Wait();
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            var redisValue = RedisHelpers.ConnectionMultiplexer.GetDatabase().StringGet("DEFAULT_THS_KeyToStore");

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

            var claim1 = new SimpleClaim { Type = "Type1", Value = "Value1" };
            var claim2 = new SimpleClaim { Type = "Type2", Value = "Value2" };

            var client = new SimpleClient
            {
                Claims = new List<SimpleClaim> { claim1, claim2 }
            };

            var token = new SimpleToken
            {
                Claims = new List<SimpleClaim> { claim1, claim2 },
                Client = client,
                Type = "Type",
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                Version = 1,
                Issuer = "Issuer",
                Lifetime = 120,
                Audience = "Audience"
            };

            var settings = new JsonSettingsFactory(new CustomMappersConfiguration()).Create();

            var serialized = JsonConvert.SerializeObject(token, settings);

            database.StringSet("DEFAULT_THS_Existing", serialized);
            database.StringSet("DEFAULT_THS_Delete", serialized);
        }

        /// <summary>
        /// Tests the fixture tear down.
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            var database = RedisHelpers.ConnectionMultiplexer.GetDatabase();

            database.KeyDelete("DEFAULT_THS_Existing");
            database.KeyDelete("DEFAULT_THS_KeyToStore");
            database.KeyDelete("DEFAULT_THS_Delete");
        }
    }
}