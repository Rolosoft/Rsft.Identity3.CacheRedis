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

// <copyright file="ClaimsPrincipalMappersTests.cs" company="Rolosoft Ltd">
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
    /// The ClaimsPrincipalMappers Tests
    /// </summary>
    [TestFixture]
    public sealed class ClaimsPrincipalMappersTests : TestBase
    {
        /// <summary>
        /// To the complex entity when simple entity expect map success.
        /// </summary>
        [Test]
        public void ToComplexEntity_WhenSimpleEntity_ExpectMapSuccess()
        {
            // Arrange
            var mockClaimsIdentityMapper = new Mock<IMapper<SimpleClaimsIdentity, ClaimsIdentity>>();
            mockClaimsIdentityMapper.Setup(r => r.ToComplexEntity(It.IsAny<IEnumerable<SimpleClaimsIdentity>>()))
                .Returns(new List<ClaimsIdentity> { new ClaimsIdentity() });

            var claimsPrincipalMappers = new ClaimsPrincipalMappers(mockClaimsIdentityMapper.Object);

            var simpleClaimsPrincipal = new SimpleClaimsPrincipal
            {
                Identities = new List<SimpleClaimsIdentity>()
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var claimsPrincipal = claimsPrincipalMappers.ToComplexEntity(simpleClaimsPrincipal);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(claimsPrincipal, Is.Not.Null);

            Assert.That(claimsPrincipal.Identities, Is.Not.Null);
            Assert.That(claimsPrincipal.Identities.Any(), Is.True);
        }

        /// <summary>
        /// To the simple entity when complex entity expect map success.
        /// </summary>
        [Test]
        public void ToSimpleEntity_WhenComplexEntity_ExpectMapSuccess()
        {
            // Arrange
            var mockClaimsIdentityMapper = new Mock<IMapper<SimpleClaimsIdentity, ClaimsIdentity>>();
            mockClaimsIdentityMapper.Setup(r => r.ToSimpleEntity(It.IsAny<IEnumerable<ClaimsIdentity>>()))
                .Returns(new List<SimpleClaimsIdentity> { new SimpleClaimsIdentity() });

            var claimsPrincipalMappers = new ClaimsPrincipalMappers(mockClaimsIdentityMapper.Object);

            var complexEntity = new ClaimsPrincipal(new List<ClaimsIdentity>());

            // Act
            var stopwatch = Stopwatch.StartNew();
            var simpleEntity = claimsPrincipalMappers.ToSimpleEntity(complexEntity);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(simpleEntity, Is.Not.Null);

            Assert.That(simpleEntity.Identities, Is.Not.Null);
            Assert.That(simpleEntity.Identities.Any(), Is.True);
        }
    }
}