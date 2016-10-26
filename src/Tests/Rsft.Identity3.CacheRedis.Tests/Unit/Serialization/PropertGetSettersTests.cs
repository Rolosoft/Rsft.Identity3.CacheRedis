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

// <copyright file="PropertGetSettersTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Unit.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using CacheRedis.Logic.Serialization;
    using NUnit.Framework;
    using TestEntities;

    /// <summary>
    /// The Propert Get Setters Tests
    /// </summary>
    [TestFixture]
    public sealed class PropertGetSettersTests : TestBase
    {
        /// <summary>
        /// Gets the getters when type passed in expect working getters.
        /// </summary>
        [Test]
        public void GetGetters_WhenTypePassedIn_ExpectWorkingGetters()
        {
            // Arrange
            var propertGetSetters = new PropertGetSetters();

            var testEntity = new GetSetterTestEntity
            {
                Int1 = 1,
                String1 = "string"
            };

            var propertyDictionary = new Dictionary<string, object>();

            // Act
            var stopwatch = Stopwatch.StartNew();
            var enumerable = propertGetSetters.GetGetters(typeof(GetSetterTestEntity));

            foreach (var func in enumerable)
            {
                propertyDictionary.Add(func.Key, func.Value(testEntity));
            }

            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(propertyDictionary["Int1"], Is.EqualTo(1));
            Assert.That(propertyDictionary["String1"], Is.EqualTo("string"));
        }

        /// <summary>
        /// Gets the setters performance test.
        /// </summary>
        [Test]
        [Repeat(2)]
        public void GetSetters_PerformanceTest()
        {
            // Arrange
            var propertGetSetters = new PropertGetSetters();

            var testEntity = new GetSetterTestEntity();

            var propertyDictionary = new Dictionary<string, object>
            {
                {"Int1", 1 },
                {"String1", "string"}
            };

            // Act and Assert
            var setters = propertGetSetters.GetSetters(typeof(GetSetterTestEntity));
            var stopwatch = Stopwatch.StartNew();

            for (var i = 0; i < 100000; i++)
            {
                foreach (var setter in setters)
                {
                    setter.Value(testEntity, propertyDictionary[setter.Key]);
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"Using Reflection: {stopwatch.ElapsedMilliseconds}ms");

            stopwatch.Restart();

            for (var i = 0; i < 100000; i++)
            {
                testEntity.Int1 = (int)propertyDictionary["Int1"];
                testEntity.String1 = (string)propertyDictionary["String1"];
            }

            stopwatch.Stop();
            Console.WriteLine($"Using Standard: {stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine("\r\n-----------------------------\r\n");
        }

        /// <summary>
        /// Gets the getters when type passed in expect working getters.
        /// </summary>
        [Test]
        public void GetSetters_WhenTypePassedIn_ExpectWorkingSetters()
        {
            // Arrange
            var propertGetSetters = new PropertGetSetters();

            var testEntity = new GetSetterTestEntity();

            var propertyDictionary = new Dictionary<string, object>
            {
                {"Int1", 1 },
                {"String1", "string"}
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var setters = propertGetSetters.GetSetters(typeof(GetSetterTestEntity));

            foreach (var setter in setters)
            {
                setter.Value(testEntity, propertyDictionary[setter.Key]);
            }

            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(testEntity.Int1, Is.EqualTo(1));
            Assert.That(testEntity.String1, Is.EqualTo("string"));
        }
    }
}