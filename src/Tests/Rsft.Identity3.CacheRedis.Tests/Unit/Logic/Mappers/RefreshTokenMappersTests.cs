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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Security.Claims;
    using CacheRedis.Logic.Mappers;
    using Entities;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using InheritedEntities;
    using Interfaces;
    using Interfaces.Serialization;
    using Moq;
    using NUnit.Framework;
    using TestHelpers;

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
            var mockPropertyMapper = new Mock<IPropertyGetSettersTyped<RefreshToken>>();
            var mockClaimsPrincipalMapper = new Mock<IMapper<SimpleClaimsPrincipal, ClaimsPrincipal>>();
            var mockTokenMapper = new Mock<IMapper<SimpleToken, Token>>();

            mockClaimsPrincipalMapper.Setup(r => r.ToComplexEntity(It.IsAny<SimpleClaimsPrincipal>()))
                .Returns(new ClaimsPrincipal());

            mockTokenMapper.Setup(r => r.ToComplexEntity(It.IsAny<SimpleToken>())).Returns(new Token());

            mockPropertyMapper.Setup(r => r.GetSetters(It.IsAny<Type>()))
                .Returns(new Dictionary<string, TypedSetter<RefreshToken>>());

            var refreshTokenMappers = new RefreshTokenMappers<RefreshToken>(
                mockPropertyMapper.Object,
                mockClaimsPrincipalMapper.Object,
                mockTokenMapper.Object);

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
        /// To the complex entity when simple entity and extended complex expect correct map.
        /// </summary>
        [Test]
        public void ToComplexEntity_WhenSimpleEntityAndExtendedComplex_ExpectCorrectMap()
        {
            // Arrange
            var mockPropertyMapper = new Mock<IPropertyGetSettersTyped<ExtendedRefreshToken>>();
            var mockClaimsPrincipalMapper = new Mock<IMapper<SimpleClaimsPrincipal, ClaimsPrincipal>>();
            var mockTokenMapper = new Mock<IMapper<SimpleToken, Token>>();

            mockClaimsPrincipalMapper.Setup(r => r.ToComplexEntity(It.IsAny<SimpleClaimsPrincipal>()))
                .Returns(new ClaimsPrincipal());

            mockTokenMapper.Setup(r => r.ToComplexEntity(It.IsAny<SimpleToken>())).Returns(new Token());

            var typedSetter = new TypedSetter<ExtendedRefreshToken>
            {
                OriginalType = typeof(int),
                Setter = typeof(ExtendedRefreshToken).GetSetter<ExtendedRefreshToken>("CustomProperty")
            };

            mockPropertyMapper.Setup(r => r.GetSetters(It.IsAny<Type>()))
                .Returns(new Dictionary<string, TypedSetter<ExtendedRefreshToken>> { { "CustomProperty", typedSetter } });

            var refreshTokenMappers = new RefreshTokenMappers<ExtendedRefreshToken>(
                mockPropertyMapper.Object,
                mockClaimsPrincipalMapper.Object,
                mockTokenMapper.Object);

            var simpleEntity = new SimpleRefreshToken
            {
                Subject = new SimpleClaimsPrincipal(),
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                AccessToken = new SimpleToken(),
                LifeTime = 1,
                Version = 1,
                DataBag = new Dictionary<string, object> { { "CustomProperty", new CustomProperty { Name = "Joe", Age = 23 } } }
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

            Assert.That(complexEntity.CustomProperty, Is.Not.Null);
            Assert.That(complexEntity.CustomProperty.Name, Is.EqualTo("Joe"));
            Assert.That(complexEntity.CustomProperty.Age, Is.EqualTo(23));
        }

        /// <summary>
        /// To the simple entity when complex entity expect correct map.
        /// </summary>
        [Test]
        public void ToSimpleEntity_WhenComplexEntity_ExpectCorrectMap()
        {
            // Arrange
            var mockPropertyMapper = new Mock<IPropertyGetSettersTyped<RefreshToken>>();
            var mockClaimsPrincipalMapper = new Mock<IMapper<SimpleClaimsPrincipal, ClaimsPrincipal>>();
            var mockTokenMapper = new Mock<IMapper<SimpleToken, Token>>();

            mockClaimsPrincipalMapper.Setup(r => r.ToSimpleEntity(It.IsAny<ClaimsPrincipal>()))
                .Returns(new SimpleClaimsPrincipal());

            mockTokenMapper.Setup(r => r.ToSimpleEntity(It.IsAny<Token>())).Returns(new SimpleToken());

            mockPropertyMapper.Setup(r => r.GetGetters(It.IsAny<Type>()))
                .Returns(new Dictionary<string, Func<RefreshToken, object>>());

            var refreshTokenMappers = new RefreshTokenMappers<RefreshToken>(
                mockPropertyMapper.Object,
                mockClaimsPrincipalMapper.Object,
                mockTokenMapper.Object);

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

        /// <summary>
        /// To the simple entity when complex entity and extended complex expect correct map.
        /// </summary>
        [Test]
        public void ToSimpleEntity_WhenComplexEntityAndExtendedComplex_ExpectCorrectMap()
        {
            // Arrange
            var mockPropertyMapper = new Mock<IPropertyGetSettersTyped<ExtendedRefreshToken>>();
            var mockClaimsPrincipalMapper = new Mock<IMapper<SimpleClaimsPrincipal, ClaimsPrincipal>>();
            var mockTokenMapper = new Mock<IMapper<SimpleToken, Token>>();

            mockClaimsPrincipalMapper.Setup(r => r.ToSimpleEntity(It.IsAny<ClaimsPrincipal>()))
                .Returns(new SimpleClaimsPrincipal());

            mockTokenMapper.Setup(r => r.ToSimpleEntity(It.IsAny<Token>())).Returns(new SimpleToken());

            var mockGetters = new Dictionary<string, Func<ExtendedRefreshToken, object>>
            {
                {
                    "CustomProperty",
                    typeof(ExtendedRefreshToken).GetProperty("CustomProperty").GetGetter<ExtendedRefreshToken>()
                }
            };

            mockPropertyMapper.Setup(r => r.GetGetters(It.IsAny<Type>())).Returns(mockGetters);

            var refreshTokenMappers = new RefreshTokenMappers<ExtendedRefreshToken>(
                mockPropertyMapper.Object,
                mockClaimsPrincipalMapper.Object,
                mockTokenMapper.Object);

            var complexEntity = new ExtendedRefreshToken
            {
                Subject = new ClaimsPrincipal(),
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                AccessToken = new Token(),
                LifeTime = 1,
                Version = 1,
                CustomProperty = new CustomProperty { Name = "Joe", Age = 23 }
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

            Assert.That(simpleRefreshToken.DataBag["CustomProperty"], Is.Not.Null);
            var property = (CustomProperty)simpleRefreshToken.DataBag["CustomProperty"];
            Assert.That(property.Name, Is.EqualTo("Joe"));
            Assert.That(property.Age, Is.EqualTo(23));
        }
    }
}