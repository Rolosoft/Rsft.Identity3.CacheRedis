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

// <copyright file="RefreshTokenStore.cs" company="Rolosoft Ltd">
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
    using IdentityServer3.Core.Models;
    using Interfaces;
    using Moq;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using TestHelpers;

    /// <summary>
    /// The Refresh Token Store Tests
    /// </summary>
    /// <seealso cref="TestBase" />
    [TestFixture]
    //  [Ignore("Set REDIS Connection string in TestHelpers.RedisHelpers to your local dev store")]
    public sealed class RefreshTokenStoreTests : TestBase
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

            var jsonSettingsFactory = new JsonSettingsFactory(new ClientMapperBase<Client>());

            var cacheManager = new RedisCacheManager<RefreshToken>(
                RedisHelpers.ConnectionMultiplexer,
                mockCacheConfiguration.Object,
                jsonSettingsFactory);

            var refreshTokenStore = new RefreshTokenStore(
                cacheManager,
                mockCacheConfiguration.Object);

            // Act
            var stopwatch = Stopwatch.StartNew();
            var refreshToken = refreshTokenStore.GetAsync("Existing").Result;
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(refreshToken, Is.Not.Null);

            Assert.That(refreshToken.Subject, Is.Not.Null);
            Assert.That(refreshToken.AccessToken, Is.Not.Null);
            Assert.That(refreshToken.CreationTime, Is.EqualTo(new DateTimeOffset(new DateTime(2016, 1, 1))));
            Assert.That(refreshToken.LifeTime, Is.EqualTo(1600));
            Assert.That(refreshToken.Scopes, Is.Not.Null);
            Assert.That(refreshToken.Version, Is.EqualTo(1));
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

            var jsonSettingsFactory = new JsonSettingsFactory(new ClientMapperBase<Client>());

            var cacheManager = new RedisCacheManager<RefreshToken>(
                RedisHelpers.ConnectionMultiplexer,
                mockCacheConfiguration.Object,
                jsonSettingsFactory);

            var refreshTokenStore = new RefreshTokenStore(
                cacheManager,
                mockCacheConfiguration.Object);

            // Act
            var stopwatch = Stopwatch.StartNew();

            refreshTokenStore.RemoveAsync("Delete").Wait();

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

            var jsonSettingsFactory = new JsonSettingsFactory(new ClientMapperBase<Client>());

            var cacheManager = new RedisCacheManager<RefreshToken>(
                RedisHelpers.ConnectionMultiplexer,
                mockCacheConfiguration.Object,
                jsonSettingsFactory);

            var refreshTokenStore = new RefreshTokenStore(
                cacheManager,
                mockCacheConfiguration.Object);

            var refreshToken = new RefreshToken
            {
                AccessToken = new Token
                {
                    Client = new Client { ClientId = "cid", },
                    Claims = new List<Claim> { new Claim("SubjectId", "sid") }
                },
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                LifeTime = 1600,
                Version = 1,
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            refreshTokenStore.StoreAsync("KeyToStore", refreshToken).Wait();
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            var redisValue = RedisHelpers.ConnectionMultiplexer.GetDatabase().StringGet("DEFAULT_RTS_KeyToStore");

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

            var refreshToken = new RefreshToken
            {
                AccessToken = new Token
                {
                    Client = new Client { ClientId = "cid", },
                    Claims = new List<Claim> { new Claim("SubjectId", "sid") }
                },
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                LifeTime = 1600,
                Version = 1,
                Subject = new ClaimsPrincipal()
            };

            var settings = new JsonSettingsFactory(new ClientMapperBase<Client>()).Create();

            var serialized = JsonConvert.SerializeObject(refreshToken, settings);

            database.StringSet("DEFAULT_RTS_Existing", serialized);
            database.StringSet("DEFAULT_RTS_Delete", serialized);
        }

        /// <summary>
        /// Tests the fixture tear down.
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            var database = RedisHelpers.ConnectionMultiplexer.GetDatabase();

            database.KeyDelete("DEFAULT_RTS_Existing");
            database.KeyDelete("DEFAULT_RTS_KeyToStore");
            database.KeyDelete("DEFAULT_RTS_Delete");
        }
    }
}