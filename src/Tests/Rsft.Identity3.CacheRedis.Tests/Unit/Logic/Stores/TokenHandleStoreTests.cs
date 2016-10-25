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

namespace Rsft.Identity3.CacheRedis.Tests.Unit.Logic.Stores
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using CacheRedis.Stores;
    using Entities;
    using IdentityServer3.Core.Models;
    using Interfaces;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The Token Handle Store Tests
    /// </summary>
    /// <seealso cref="TestBase" />
    [TestFixture]
    public sealed class TokenHandleStoreTests : TestBase
    {
        /// <summary>
        /// Gets all asynchronous when called expect throws.
        /// </summary>
        [Test]
        public void GetAllAsync_WhenCalled_ExpectThrows()
        {
            // Arrange
            var mockCacheManager = new Mock<ICacheManager<Token>>();
            var mockCacheConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();

            mockCacheConfiguration.Setup(r => r.Get).Returns(new RedisCacheConfigurationEntity { CacheDuration = 1, RefreshTokenCacheDuration = 1 });

            var tokenHandleStore = new TokenHandleStore(
                mockCacheManager.Object,
                mockCacheConfiguration.Object);

            // Act and Assert
            var stopwatch = Stopwatch.StartNew();

            Assert.Throws<NotImplementedException>(
                () => tokenHandleStore.GetAllAsync("string").GetAwaiter().GetResult());

            stopwatch.Stop();
            this.WriteTimeElapsed(stopwatch);
        }

        /// <summary>
        /// Gets the asynchronous when called expect response.
        /// </summary>
        [Test]
        public void GetAsync_WhenCalled_ExpectResponse()
        {
            // Arrange
            var mockCacheManager = new Mock<ICacheManager<Token>>();
            var mockCacheConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();

            mockCacheConfiguration.Setup(r => r.Get).Returns(new RedisCacheConfigurationEntity { CacheDuration = 1, RefreshTokenCacheDuration = 1 });

            var keyCallback = default(string);
            mockCacheManager.Setup(r => r.GetAsync(It.IsAny<string>()))
                .Callback((string s) => keyCallback = s)
                .ReturnsAsync(new Token());

            var tokenHandleStore = new TokenHandleStore(
                mockCacheManager.Object,
                mockCacheConfiguration.Object);

            // Act
            var stopwatch = Stopwatch.StartNew();

            var authorizationCode = tokenHandleStore.GetAsync("string").Result;

            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(authorizationCode, Is.Not.Null);
            Assert.That(keyCallback, Is.EqualTo("THS_string"));
        }

        /// <summary>
        /// Removes the asynchronous when called expect response.
        /// </summary>
        [Test]
        public void RemoveAsync_WhenCalled_ExpectAction()
        {
            // Arrange
            var mockCacheManager = new Mock<ICacheManager<Token>>();
            var mockCacheConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();

            mockCacheConfiguration.Setup(r => r.Get).Returns(new RedisCacheConfigurationEntity { CacheDuration = 1, RefreshTokenCacheDuration = 1 });

            var keyCallback = default(string);
            mockCacheManager.Setup(r => r.DeleteAsync(It.IsAny<string>()))
                .Callback((string s) => keyCallback = s)
                .Returns(Task.FromResult(0));

            var tokenHandleStore = new TokenHandleStore(
                mockCacheManager.Object,
                mockCacheConfiguration.Object);

            // Act
            var stopwatch = Stopwatch.StartNew();

            tokenHandleStore.RemoveAsync("string").Wait();

            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(keyCallback, Is.EqualTo("THS_string"));
            mockCacheManager.Verify(r => r.DeleteAsync(It.IsAny<string>()), Times.Once());
        }

        /// <summary>
        /// Revokes the asynchronous when called expect throws.
        /// </summary>
        [Test]
        public void RevokeAsync_WhenCalled_ExpectThrows()
        {
            // Arrange
            var mockCacheManager = new Mock<ICacheManager<Token>>();
            var mockCacheConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();

            mockCacheConfiguration.Setup(r => r.Get).Returns(new RedisCacheConfigurationEntity { CacheDuration = 1, RefreshTokenCacheDuration = 1 });

            var tokenHandleStore = new TokenHandleStore(
                mockCacheManager.Object,
                mockCacheConfiguration.Object);

            // Act and Assert
            var stopwatch = Stopwatch.StartNew();

            Assert.Throws<NotImplementedException>(
                () => tokenHandleStore.RevokeAsync("string", "string").GetAwaiter().GetResult());

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
            var mockCacheManager = new Mock<ICacheManager<Token>>();
            var mockCacheConfiguration = new Mock<IConfiguration<RedisCacheConfigurationEntity>>();

            mockCacheConfiguration.Setup(r => r.Get).Returns(new RedisCacheConfigurationEntity { CacheDuration = 1, RefreshTokenCacheDuration = 2 });

            var keyCallback = default(string);
            var timespanCallback = default(TimeSpan);
            mockCacheManager.Setup(r => r.SetAsync(It.IsAny<string>(), It.IsAny<Token>(), It.IsAny<TimeSpan>()))
                .Callback((string s, Token a, TimeSpan t) =>
                {
                    keyCallback = s;
                    timespanCallback = t;
                })
                .Returns(Task.FromResult(0));

            var tokenHandleStore = new TokenHandleStore(
                mockCacheManager.Object,
                mockCacheConfiguration.Object);

            // Act
            var stopwatch = Stopwatch.StartNew();

            tokenHandleStore.StoreAsync("string", new Token { Lifetime = 100 }).Wait();

            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(keyCallback, Is.EqualTo("THS_string"));
            Assert.That(timespanCallback, Is.EqualTo(TimeSpan.FromSeconds(95)));
            mockCacheManager.Verify(r => r.SetAsync(It.IsAny<string>(), It.IsAny<Token>(), It.IsAny<TimeSpan>()), Times.Once());
        }
    }
}