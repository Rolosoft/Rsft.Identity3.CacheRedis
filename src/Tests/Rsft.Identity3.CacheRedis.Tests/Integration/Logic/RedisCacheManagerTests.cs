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

// <copyright file="RedisCacheManagerTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Integration.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Tracing;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CacheRedis.Logic;
    using Diagnostics.EventSources;
    using Entities;

    using IdentityServer3.Core.Models;
    using Interfaces;
    using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
    using Moq;

    using Newtonsoft.Json;

    using NUnit.Framework;
    using TestHelpers;

    /// <summary>
    /// The Redis Cache Manager Tests
    /// </summary>
    /// <seealso cref="TestBase" />
    [TestFixture]
    //   [Ignore("Set REDIS Connection string in TestHelpers.RedisHelpers to your local dev store")]
    public class RedisCacheManagerTests : TestBase
    {
        /// <summary>
        /// The activity event listener
        /// </summary>
        private static ObservableEventListener activityEventListener;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheManagerTests"/> class.
        /// </summary>
        public RedisCacheManagerTests()
        {
            activityEventListener = new ObservableEventListener();
            activityEventListener.EnableEvents(ActivityLoggingEventSource.Log, EventLevel.LogAlways);
            activityEventListener.LogToConsole();
        }

        /// <summary>
        /// Sets the asynchronous when claim object expect success.
        /// </summary>
        [Test]
        public void SetAsync_WhenClaimObject_ExpectSuccess()
        {
            // arrange
            const string Key = @"SetAsync_WhenClaimObject_ExpectSuccess";

            var claim = new Claim("mytype1", "myvalue2");

            var claims = new List<Claim> { claim, claim, claim };

            var timeSpan = TimeSpan.FromSeconds(10);

            var mockConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();
            mockConfiguration.Setup(r => r.Get)
                .Returns(
                    () =>
                        new RedisCacheConfigurationEntity
                        {
                            CacheDuration = 3600,
                            UseObjectCompression = true,
                            RedisCacheDefaultPrefix = @"RedisCacheManagerTests"
                        });

            var jsonSettingsFactory = new JsonSettingsFactory(new ClientMapperBase<Client>());

            // act
            var redisCacheManager = new RedisCacheManager<IEnumerable<Claim>>(
                RedisHelpers.ConnectionMultiplexer,
                mockConfiguration.Object,
                jsonSettingsFactory);
            redisCacheManager.SetAsync(Key, claims, timeSpan).Wait();

            var result = redisCacheManager.GetAsync(Key).Result.ToList();

            // assert
            Console.WriteLine(@"Fetched From Cache:{0}", JsonConvert.SerializeObject(result));
            Assert.That(result.GetType(), Is.EqualTo(typeof(List<Claim>)));
        }

        [Test]
        public void SetAsync_WhenClaimObjectAndTtlExceeded_ExpectNull()
        {
            // arrange
            const string Key = @"SetAsync_WhenClaimObjectAndTtlExceeded_ExpectNull";

            var claim = new Claim("mytype1", "myvalue2");

            var claims = new List<Claim> { claim, claim, claim };

            var timeSpan = TimeSpan.FromSeconds(5);

            var mockConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();
            mockConfiguration.Setup(r => r.Get).Returns(() => new RedisCacheConfigurationEntity { CacheDuration = 3600, UseObjectCompression = true, RedisCacheDefaultPrefix = @"RedisCacheManagerTests" });

            var mockJsonSettingsFactory = new Mock<IJsonSettingsFactory>();

            // act
            var redisCacheManager = new RedisCacheManager<IEnumerable<Claim>>(
                RedisHelpers.ConnectionMultiplexer,
                mockConfiguration.Object,
                mockJsonSettingsFactory.Object);
            redisCacheManager.SetAsync(Key, claims, timeSpan).Wait();

            /*Wait 6 seconds for expiry*/
            Task.Delay(6000).Wait();

            var result = redisCacheManager.GetAsync(Key).Result;

            // assert
            Console.WriteLine(@"Fetched From Cache:{0}", JsonConvert.SerializeObject(result));
            Assert.That(result == null);
        }

        [Test]
        public void SetAsync_WhenClientObject_ExpectSuccess()
        {
            // arrange
            const string Key = @"SetAsync_WhenClientObject_ExpectSuccess";
            var client = new Client { AllowedScopes = new List<string> { "scope 1", "scope 2" }, Enabled = true };

            var timeSpan = TimeSpan.FromSeconds(10);

            var mockConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();
            mockConfiguration.Setup(r => r.Get).Returns(() => new RedisCacheConfigurationEntity { CacheDuration = 3600, UseObjectCompression = true, RedisCacheDefaultPrefix = @"RedisCacheManagerTests" });

            var mockJsonSettingsFactory = new Mock<IJsonSettingsFactory>();

            // act
            var redisCacheManager = new RedisCacheManager<Client>(
                RedisHelpers.ConnectionMultiplexer,
                mockConfiguration.Object,
                mockJsonSettingsFactory.Object);
            redisCacheManager.SetAsync(Key, client, timeSpan).Wait();

            var result = redisCacheManager.GetAsync(Key).Result;

            // assert
            Console.WriteLine(@"Fetched From Cache:{0}", JsonConvert.SerializeObject(result));
            Assert.That(result.GetType() == typeof(Client));
        }

        [Test]
        public void SetAsync_WhenScopeObject_ExpectSuccess()
        {
            // arrange
            const string Key = @"SetAsync_WhenScopeObject_ExpectSuccess";
            var scope = new Scope { Claims = new List<ScopeClaim> { new ScopeClaim(@"fdf") } };

            var scopes = new List<Scope> { scope, scope, scope };

            var timeSpan = TimeSpan.FromSeconds(10);

            var mockConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();
            mockConfiguration.Setup(r => r.Get).Returns(() => new RedisCacheConfigurationEntity { CacheDuration = 3600, UseObjectCompression = true, RedisCacheDefaultPrefix = @"RedisCacheManagerTests" });

            var mockJsonSettingsFactory = new Mock<IJsonSettingsFactory>();

            // act
            var redisCacheManager = new RedisCacheManager<IEnumerable<Scope>>(
                RedisHelpers.ConnectionMultiplexer,
                mockConfiguration.Object,
                mockJsonSettingsFactory.Object);
            redisCacheManager.SetAsync(Key, scopes, timeSpan).Wait();

            var result = redisCacheManager.GetAsync(Key).Result;

            // assert
            Console.WriteLine(@"Fetched From Cache:{0}", JsonConvert.SerializeObject(result));
            Assert.That(result.GetType() == typeof(List<Scope>));
        }

        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            activityEventListener.DisableEvents(ActivityLoggingEventSource.Log);

            activityEventListener.Dispose();
        }
    }
}