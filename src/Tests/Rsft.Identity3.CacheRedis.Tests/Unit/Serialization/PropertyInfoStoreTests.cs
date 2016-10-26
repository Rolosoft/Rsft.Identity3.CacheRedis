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

// <copyright file="PropertyInfoStoreTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Unit.Serialization
{
    using System.Diagnostics;
    using System.Linq;
    using CacheRedis.Logic.Serialization;
    using NUnit.Framework;
    using TestEntities;

    /// <summary>
    /// The Property Info Store Tests
    /// </summary>
    [TestFixture]
    public sealed class PropertyInfoStoreTests : TestBase
    {
        /// <summary>
        /// Gets the declared properties when inherited class expect only declared.
        /// </summary>
        [Test]
        [Repeat(2)]
        public void GetDeclaredProperties_WhenInheritedClass_ExpectOnlyDeclared()
        {
            // Arrange
            var propertyInfoStore = new PropertyInfoStore();

            var propertyInfoStoreTestEntity = new PropertyInfoStoreTestEntity();

            // Act
            var stopwatch = Stopwatch.StartNew();
            var declaredProperties = propertyInfoStore.GetDeclaredProperties(propertyInfoStoreTestEntity).ToList();
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(declaredProperties.Count, Is.EqualTo(1));
            Assert.That(declaredProperties.First().Name, Is.EqualTo("NewValue"));
        }
    }
}