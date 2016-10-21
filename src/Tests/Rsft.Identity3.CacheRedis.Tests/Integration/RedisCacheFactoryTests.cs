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

// <copyright file="RedisCacheFactoryTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Tracing;
    using System.Security.Claims;

    using Diagnostics.EventSources;

    using IdentityServer3.Core.Configuration;
    using IdentityServer3.Core.Models;
    using IdentityServer3.Core.Services;
    using IdentityServer3.Core.Services.InMemory;

    using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
    using NUnit.Framework;
    using StackExchange.Redis;

    //    [Ignore("Supply a valid Redis connection string to test. Tests verified passing on 05/23/16 - ROC")]
    [TestFixture]
    public class RedisCacheFactoryTests : TestBase
    {
        //       private const string RedisConnectionString = @"<INSERT CONNECTION STRING HERE>";
        private const string RedisConnectionString = @"127.0.0.1:6379";

        private static readonly Lazy<ConnectionMultiplexer> ConnectionMuxLazy = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(RedisConnectionString));

        private static ObservableEventListener activityEventListener;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheFactoryTests"/> class.
        /// </summary>
        public RedisCacheFactoryTests()
        {
            activityEventListener = new ObservableEventListener();
            activityEventListener.EnableEvents(ActivityLoggingEventSource.Log, EventLevel.LogAlways);
            activityEventListener.LogToConsole();
        }

        private static ConnectionMultiplexer Singleton => ConnectionMuxLazy.Value;

        [Test]
        public void Create_WhenConnectionString_ExpectCachesCreated()
        {
            // arrange

            // act
            var caches = RedisCacheFactory.Create(RedisConnectionString);

            // assert
            Assert.IsNotNull(caches);
            Assert.IsNotNull(caches.ScopesCache);
            Assert.IsNotNull(caches.ClientCache);
            Assert.IsNotNull(caches.UserServiceCache);

            Assert.IsInstanceOf<ICache<IEnumerable<Scope>>>(caches.ScopesCache);
            Assert.IsInstanceOf<ICache<Client>>(caches.ClientCache);
            Assert.IsInstanceOf<ICache<IEnumerable<Claim>>>(caches.UserServiceCache);
        }

        [Test]
        public void IdentityServer_Configure_WhenFactory_ExpectNoExceptions()
        {
            // arrange
            var caches = RedisCacheFactory.Create(RedisConnectionString);

            var clientCache = caches.ClientCache;
            var scopesCache = caches.ScopesCache;
            var userServiceCache = caches.UserServiceCache;

            var identityServerServiceFactory = new IdentityServerServiceFactory().UseInMemoryClients(new List<Client>()).UseInMemoryScopes(new List<Scope>()).UseInMemoryUsers(new List<InMemoryUser>());

            Debug.Assert(clientCache != null, "clientCache != null");
            Debug.Assert(scopesCache != null, "scopesCache != null");
            Debug.Assert(userServiceCache != null, "userServiceCache != null");

            // act
            identityServerServiceFactory.ConfigureClientStoreCache(new Registration<ICache<Client>>(clientCache));
            identityServerServiceFactory.ConfigureScopeStoreCache(new Registration<ICache<IEnumerable<Scope>>>(scopesCache));
            identityServerServiceFactory.ConfigureUserServiceCache(new Registration<ICache<IEnumerable<Claim>>>(userServiceCache));

            // assert
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