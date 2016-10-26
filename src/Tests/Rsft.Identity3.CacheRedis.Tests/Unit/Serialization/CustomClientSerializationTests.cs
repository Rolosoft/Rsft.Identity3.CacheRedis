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

// <copyright file="CustomClientSerializationTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Unit.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using CacheRedis.Logic.Serialization;
    using IdentityServer3.Core.Models;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using SuperSimpleTests;
    using TestHelpers;

    /// <summary>
    /// The Custom Client Serialization Tests
    /// </summary>
    [TestFixture]
    public sealed class CustomClientSerializationTests : TestBase
    {
        [Test]
        [Repeat(2)]
        public void Control()
        {
            var sw = Stopwatch.StartNew();

            for (var i = 0; i < 1000000; i++)
            {
                var client = new Client();
            }

            sw.Stop();

            Console.WriteLine($"Elapsed {sw.ElapsedMilliseconds}ms ({sw.Elapsed})");
        }

        /// <summary>
        /// Serializes the when serialized and deserialized expect correct result.
        /// </summary>
        [Test]
        [Repeat(2)]
        public void Serialize_WhenSerializedAndDeserialized_ExpectCorrectResult()
        {
            var type = typeof(CustomClient);

            var simple = new CustomSimple
            {
                DataBag = new Dictionary<string, object> { { "AppId", (int)12 } },
                ClientId = "cid",
                Type = type
            };

            var jsonSerializerSettings = new JsonSerializerSettings();

            var inputMapper = new CustomClientInputMapper(null);
            var outputMapper = new CustomClientOutputMapper(null);

            jsonSerializerSettings.Converters.Add(new CustomClientConverter(inputMapper, outputMapper));

            var serializeObject = JsonConvert.SerializeObject(simple, jsonSerializerSettings);

            var deserializeObject = (CustomClient)JsonConvert.DeserializeObject<CustomClient>(serializeObject, jsonSerializerSettings);

            var sw = Stopwatch.StartNew();

            sw.Stop();

            Console.WriteLine($"Elapsed {sw.ElapsedMilliseconds}ms ({sw.Elapsed})");
        }
    }
}