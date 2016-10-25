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

// <copyright file="ClaimMappersTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Unit.Logic.Mappers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Security.Claims;
    using CacheRedis.Logic.Mappers;
    using Entities.Serialization;
    using NUnit.Framework;

    /// <summary>
    /// The Claim Mappers Tests
    /// </summary>
    [TestFixture]
    public sealed class ClaimMappersTests : TestBase
    {
        /// <summary>
        /// To the complex entity when simple entity expect correct map.
        /// </summary>
        [Test]
        public void ToComplexEntity_WhenSimpleEntity_ExpectCorrectMap()
        {
            // Arrange
            var claimMappers = new ClaimMappers();

            var simpleEntity = new SimpleClaim
            {
                Type = "Type",
                Issuer = "Issuer",
                ValueType = "ValueType",
                Properties = new Dictionary<string, string>(),
                Value = "Value",
                OriginalIssuer = "OriginalIssuer"
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var complexEntity = claimMappers.ToComplexEntity(simpleEntity);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(complexEntity, Is.Not.Null);

            Assert.That(complexEntity.Type, Is.EqualTo("Type"));
            Assert.That(complexEntity.Issuer, Is.EqualTo("Issuer"));
            Assert.That(complexEntity.ValueType, Is.EqualTo("ValueType"));
            Assert.That(complexEntity.Properties, Is.Not.Null);
            Assert.That(complexEntity.Value, Is.EqualTo("Value"));
            Assert.That(complexEntity.OriginalIssuer, Is.EqualTo("OriginalIssuer"));
        }

        /// <summary>
        /// To the simple entity when complex entity expect correct map.
        /// </summary>
        [Test]
        public void ToSimpleEntity_WhenComplexEntity_ExpectCorrectMap()
        {
            // Arrange
            var claimMappers = new ClaimMappers();

            var complexEntity = new Claim("Type", "Value", "ValueType", "Issuer", "OriginalIssuer");

            // Act
            var stopwatch = Stopwatch.StartNew();
            var simpleEntity = claimMappers.ToSimpleEntity(complexEntity);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(simpleEntity, Is.Not.Null);

            Assert.That(simpleEntity.Type, Is.EqualTo("Type"));
            Assert.That(simpleEntity.Issuer, Is.EqualTo("Issuer"));
            Assert.That(simpleEntity.ValueType, Is.EqualTo("ValueType"));
            Assert.That(simpleEntity.Properties, Is.Not.Null);
            Assert.That(simpleEntity.Value, Is.EqualTo("Value"));
            Assert.That(simpleEntity.OriginalIssuer, Is.EqualTo("OriginalIssuer"));
        }
    }
}