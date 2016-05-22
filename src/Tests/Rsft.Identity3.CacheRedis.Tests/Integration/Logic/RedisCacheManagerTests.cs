// <copyright file="RedisCacheManagerTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Tests.Integration.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Tracing;
    using System.Security.Claims;
    using CacheRedis.Logic;
    using Diagnostics.EventSources;
    using Entities;

    using IdentityServer3.Core.Models;
    using Interfaces;
    using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
    using Moq;

    using Newtonsoft.Json;

    using NUnit.Framework;
    using StackExchange.Redis;

    [Ignore("Enter your own Redis connection string.")]
    [TestFixture]
    public class RedisCacheManagerTests : TestBase
    {
        private const string RedisConnectionString = @"<INSERT CONNECTION STRING HERE>";

        private static readonly Lazy<ConnectionMultiplexer> ConnectionMuxLazy = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(RedisConnectionString));

        private static ObservableEventListener activityEventListener;

        public RedisCacheManagerTests()
        {
            activityEventListener = new ObservableEventListener();
            activityEventListener.EnableEvents(ActivityLoggingEventSource.Log, EventLevel.LogAlways);
            activityEventListener.LogToConsole();
        }

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        /// <value>
        /// The singleton.
        /// </value>
        private static ConnectionMultiplexer Singleton => ConnectionMuxLazy.Value;

        [Test]
        public void SetAsync_WhenClaimObject_ExpectSuccess()
        {
            // arrange
            const string Key = @"SetAsync_WhenClaimObject_ExpectSuccess";

            var claim = new Claim("mytype1", "myvalue2");

            var timeSpan = DateTime.UtcNow.AddMilliseconds(10000) - DateTime.UtcNow;

            var mockConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();
            mockConfiguration.Setup(r => r.Get).Returns(() => new RedisCacheConfigurationEntity { CacheDuration = 1000, UseObjectCompression = true, RedisCacheDefaultPrefix = @"RedisCacheManagerTests" });

            // act
            var redisCacheManager = new RedisCacheManager<Claim>(Singleton, mockConfiguration.Object);
            redisCacheManager.SetAsync(Key, claim, timeSpan).Wait();

            var result = redisCacheManager.GetAsync(Key).Result;

            // assert
            Console.WriteLine(@"Fetched From Cache:{0}", JsonConvert.SerializeObject(result));
            Assert.That(result.GetType() == typeof(Claim));
        }

        [Test]
        public void SetAsync_WhenClientObject_ExpectSuccess()
        {
            // arrange
            const string Key = @"SetAsync_WhenClientObject_ExpectSuccess";
            var client = new Client { AllowedScopes = new List<string> { "scope 1", "scope 2" }, Enabled = true};

            var timeSpan = DateTime.UtcNow.AddMilliseconds(10000) - DateTime.UtcNow;

            var mockConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();
            mockConfiguration.Setup(r => r.Get).Returns(() => new RedisCacheConfigurationEntity { CacheDuration = 1000, UseObjectCompression = true, RedisCacheDefaultPrefix = @"RedisCacheManagerTests" });

            // act
            var redisCacheManager = new RedisCacheManager<Client>(Singleton, mockConfiguration.Object);
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
            var scope = new Scope {Claims = new List<ScopeClaim> { new ScopeClaim(@"fdf") } };

            var timeSpan = DateTime.UtcNow.AddMilliseconds(10000) - DateTime.UtcNow;

            var mockConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();
            mockConfiguration.Setup(r => r.Get).Returns(() => new RedisCacheConfigurationEntity { CacheDuration = 1000, UseObjectCompression = true, RedisCacheDefaultPrefix = @"RedisCacheManagerTests" });

            // act
            var redisCacheManager = new RedisCacheManager<Scope>(Singleton, mockConfiguration.Object);
            redisCacheManager.SetAsync(Key, scope, timeSpan).Wait();

            var result = redisCacheManager.GetAsync(Key).Result;

            // assert
            Console.WriteLine(@"Fetched From Cache:{0}", JsonConvert.SerializeObject(result));
            Assert.That(result.GetType() == typeof(Scope));
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

            try
            {
                Singleton.Close();
                Singleton.Dispose();
            }
            catch (Exception) { }

            activityEventListener.Dispose();
        }
    }
}