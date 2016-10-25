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

// <copyright file="RefreshTokenMappersTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Unit.Logic.Mappers
{
    using System;
    using System.Diagnostics;
    using System.Security.Claims;
    using CacheRedis.Logic.Mappers;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The Refresh Token Mappers Tests
    /// </summary>
    [TestFixture]
    public sealed class RefreshTokenMappersTests : TestBase
    {
        /// <summary>
        /// To the complex entity when simple entity expect correct map.
        /// </summary>
        [Test]
        public void ToComplexEntity_WhenSimpleEntity_ExpectCorrectMap()
        {
            // Arrange
            var mockClaimsPrincipalMapper = new Mock<IMapper<SimpleClaimsPrincipal, ClaimsPrincipal>>();
            var mockTokenMapper = new Mock<IMapper<SimpleToken, Token>>();

            mockClaimsPrincipalMapper.Setup(r => r.ToComplexEntity(It.IsAny<SimpleClaimsPrincipal>()))
                .Returns(new ClaimsPrincipal());

            mockTokenMapper.Setup(r => r.ToComplexEntity(It.IsAny<SimpleToken>())).Returns(new Token());

            var refreshTokenMappers = new RefreshTokenMappers(mockClaimsPrincipalMapper.Object, mockTokenMapper.Object);

            var simpleEntity = new SimpleRefreshToken
            {
                Subject = new SimpleClaimsPrincipal(),
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                AccessToken = new SimpleToken(),
                LifeTime = 1,
                Version = 1
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var complexEntity = refreshTokenMappers.ToComplexEntity(simpleEntity);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(complexEntity, Is.Not.Null);

            Assert.That(complexEntity.Subject, Is.Not.Null);
            Assert.That(complexEntity.AccessToken, Is.Not.Null);
            Assert.That(complexEntity.CreationTime, Is.EqualTo(new DateTimeOffset(new DateTime(2016, 1, 1))));
            Assert.That(complexEntity.LifeTime, Is.EqualTo(1));
            Assert.That(complexEntity.Version, Is.EqualTo(1));
        }

        /// <summary>
        /// To the simple entity when complex entity expect correct map.
        /// </summary>
        [Test]
        public void ToSimpleEntity_WhenComplexEntity_ExpectCorrectMap()
        {
            // Arrange
            var mockClaimsPrincipalMapper = new Mock<IMapper<SimpleClaimsPrincipal, ClaimsPrincipal>>();
            var mockTokenMapper = new Mock<IMapper<SimpleToken, Token>>();

            mockClaimsPrincipalMapper.Setup(r => r.ToSimpleEntity(It.IsAny<ClaimsPrincipal>()))
                .Returns(new SimpleClaimsPrincipal());

            mockTokenMapper.Setup(r => r.ToSimpleEntity(It.IsAny<Token>())).Returns(new SimpleToken());

            var refreshTokenMappers = new RefreshTokenMappers(mockClaimsPrincipalMapper.Object, mockTokenMapper.Object);

            var complexEntity = new RefreshToken
            {
                Subject = new ClaimsPrincipal(),
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                AccessToken = new Token(),
                LifeTime = 1,
                Version = 1
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var simpleRefreshToken = refreshTokenMappers.ToSimpleEntity(complexEntity);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(simpleRefreshToken.Subject, Is.Not.Null);
            Assert.That(simpleRefreshToken.AccessToken, Is.Not.Null);
            Assert.That(simpleRefreshToken.CreationTime, Is.EqualTo(new DateTimeOffset(new DateTime(2016, 1, 1))));
            Assert.That(simpleRefreshToken.LifeTime, Is.EqualTo(1));
            Assert.That(simpleRefreshToken.Version, Is.EqualTo(1));
        }
    }
}