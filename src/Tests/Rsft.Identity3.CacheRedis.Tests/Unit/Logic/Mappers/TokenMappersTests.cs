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

// <copyright file="TokenMappersTests.cs" company="Rolosoft Ltd">
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
    using Interfaces.Serialization;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The Refresh Token Mappers Tests
    /// </summary>
    [TestFixture]
    public sealed class TokenMappersTests : TestBase
    {
        /// <summary>
        /// To the complex entity when simple entity expect correct map.
        /// </summary>
        [Test]
        public void ToComplexEntity_WhenSimpleEntity_ExpectCorrectMap()
        {
            // Arrange
            var mockPropertyMapper = new Mock<IPropertyGetSettersTyped<Token>>();
            var mockClaimsMapper = new Mock<IMapper<SimpleClaim, Claim>>();
            var mockClientMapper = new Mock<IMapper<SimpleClient, Client>>();

            mockClaimsMapper.Setup(r => r.ToComplexEntity(It.IsAny<SimpleClaim>())).Returns(new Claim("Val1", "Val2"));
            mockClientMapper.Setup(r => r.ToComplexEntity(It.IsAny<SimpleClient>())).Returns(new Client());

            var tokenMappers = new TokenMapper<Token>(mockPropertyMapper.Object, mockClaimsMapper.Object, mockClientMapper.Object);

            var simpleEntity = new SimpleToken
            {
                Claims = new List<SimpleClaim>(),
                Client = new SimpleClient(),
                Type = "Type",
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                Issuer = "Issuer",
                Version = 1,
                Audience = "Audience",
                Lifetime = 1,
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var complexEntity = tokenMappers.ToComplexEntity(simpleEntity);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(complexEntity, Is.Not.Null);

            Assert.That(complexEntity.Claims, Is.Not.Null);
            Assert.That(complexEntity.Client, Is.Not.Null);
            Assert.That(complexEntity.Type, Is.EqualTo("Type"));
            Assert.That(complexEntity.CreationTime, Is.EqualTo(new DateTimeOffset(new DateTime(2016, 1, 1))));
            Assert.That(complexEntity.Issuer, Is.EqualTo("Issuer"));
            Assert.That(complexEntity.Version, Is.EqualTo(1));
            Assert.That(complexEntity.Audience, Is.EqualTo("Audience"));
            Assert.That(complexEntity.Lifetime, Is.EqualTo(1));
        }

        /// <summary>
        /// To the simple entity when complex entity expect correct map.
        /// </summary>
        [Test]
        public void ToSimpleEntity_WhenComplexEntity_ExpectCorrectMap()
        {
            // Arrange
            var mockPropertyMapper = new Mock<IPropertyGetSettersTyped<Token>>();

            var mockClaimsMapper = new Mock<IMapper<SimpleClaim, Claim>>();
            var mockClientMapper = new Mock<IMapper<SimpleClient, Client>>();

            mockPropertyMapper.Setup(r => r.GetGetters(It.IsAny<Type>()))
                .Returns(new Dictionary<string, Func<Token, object>>());

            mockClaimsMapper.Setup(r => r.ToSimpleEntity(It.IsAny<Claim>())).Returns(new SimpleClaim());
            mockClientMapper.Setup(r => r.ToSimpleEntity(It.IsAny<Client>())).Returns(new SimpleClient());

            var tokenMappers = new TokenMapper<Token>(
                mockPropertyMapper.Object,
                mockClaimsMapper.Object,
                mockClientMapper.Object);

            var complexEntity = new Token
            {
                Claims = new List<Claim>(),
                Client = new Client(),
                Type = "Type",
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                Issuer = "Issuer",
                Version = 1,
                Audience = "Audience",
                Lifetime = 1
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var simpleRefreshToken = tokenMappers.ToSimpleEntity(complexEntity);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(simpleRefreshToken.Claims, Is.Not.Null);
            Assert.That(simpleRefreshToken.Client, Is.Not.Null);
            Assert.That(simpleRefreshToken.Type, Is.EqualTo("Type"));
            Assert.That(simpleRefreshToken.CreationTime, Is.EqualTo(new DateTimeOffset(new DateTime(2016, 1, 1))));
            Assert.That(simpleRefreshToken.Issuer, Is.EqualTo("Issuer"));
            Assert.That(simpleRefreshToken.Version, Is.EqualTo(1));
            Assert.That(simpleRefreshToken.Audience, Is.EqualTo("Audience"));
            Assert.That(simpleRefreshToken.Lifetime, Is.EqualTo(1));
        }
    }
}