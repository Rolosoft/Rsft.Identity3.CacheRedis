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

// <copyright file="ClaimsIdentityMappersTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Unit.Logic.Mappers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Claims;
    using CacheRedis.Logic.Mappers;
    using Entities.Serialization;
    using Interfaces;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The Claims Identity Mappers Tests
    /// </summary>
    [TestFixture]
    public class ClaimsIdentityMappersTests : TestBase
    {
        /// <summary>
        /// To the complex entity when simple entity expect correct map.
        /// </summary>
        [Test]
        public void ToComplexEntity_WhenSimpleEntity_ExpectCorrectMap()
        {
            // Arrange
            var mockClaimMappers = new Mock<IMapper<SimpleClaim, Claim>>();

            mockClaimMappers.Setup(r => r.ToComplexEntity(It.IsAny<IEnumerable<SimpleClaim>>()))
                .Returns(new List<Claim> { new Claim("val1", "val2") });

            var claimMappers = new ClaimsIdentityMappers(mockClaimMappers.Object);

            var simpleEntity = new SimpleClaimsIdentity
            {
                Claims = new List<SimpleClaim>(),
                AuthenticationType = "AuthenticationType",
                BootstrapContext = "BootstrapContext",
                Label = "Label",
                RoleClaimType = "RoleClaimType",
                NameClaimType = "NameClaimType"
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var complexEntity = claimMappers.ToComplexEntity(simpleEntity);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(complexEntity, Is.Not.Null);

            Assert.That(complexEntity.Claims, Is.Not.Null);
            Assert.That(complexEntity.Claims.Any(), Is.True);
            Assert.That(complexEntity.AuthenticationType, Is.EqualTo("AuthenticationType"));
            Assert.That(complexEntity.BootstrapContext, Is.EqualTo("BootstrapContext"));
            Assert.That(complexEntity.Label, Is.EqualTo("Label"));
            Assert.That(complexEntity.RoleClaimType, Is.EqualTo("RoleClaimType"));
            Assert.That(complexEntity.NameClaimType, Is.EqualTo("NameClaimType"));
        }

        /// <summary>
        /// To the simple entity when complex entity expect correct map.
        /// </summary>
        [Test]
        public void ToSimpleEntity_WhenComplexEntity_ExpectCorrectMap()
        {
            // Arrange
            var mockClaimMappers = new Mock<IMapper<SimpleClaim, Claim>>();

            mockClaimMappers.Setup(r => r.ToSimpleEntity(It.IsAny<IEnumerable<Claim>>()))
                .Returns(new List<SimpleClaim> { new SimpleClaim() });

            var claimMappers = new ClaimsIdentityMappers(mockClaimMappers.Object);

            var complexEntity = new ClaimsIdentity(new List<Claim>(), "AuthenticationType", "NameClaimType", "RoleClaimType")
            {
                BootstrapContext = "BootstrapContext",
                Label = "Label"
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var simpleEntity = claimMappers.ToSimpleEntity(complexEntity);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(simpleEntity, Is.Not.Null);

            Assert.That(simpleEntity.Claims, Is.Not.Null);
            Assert.That(simpleEntity.Claims.Any(), Is.True);
            Assert.That(simpleEntity.AuthenticationType, Is.EqualTo("AuthenticationType"));
            Assert.That(simpleEntity.BootstrapContext, Is.EqualTo("BootstrapContext"));
            Assert.That(simpleEntity.Label, Is.EqualTo("Label"));
            Assert.That(simpleEntity.RoleClaimType, Is.EqualTo("RoleClaimType"));
            Assert.That(simpleEntity.NameClaimType, Is.EqualTo("NameClaimType"));
        }
    }
}