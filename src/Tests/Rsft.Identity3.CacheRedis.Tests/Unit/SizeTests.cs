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

// <copyright file="SizeTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CacheRedis.Logic;
    using Entities;
    using Entities.Serialization;
    using Helpers;
    using Newtonsoft.Json;
    using NUnit.Framework;

    [TestFixture]
    public class SizeTests : TestBase
    {
        /// <summary>
        /// Tests the sizes compress.
        /// </summary>
        [Test]
        public void TestSizesCompress()
        {
            var code = new SimpleAuthorizationCode
            {
                Client = new SimpleClient
                {
                    ClientId = "cid"
                },
                RequestedScopes = new List<SimpleScope> { new SimpleScope { Description = "this is description", Enabled = true, Name = "Scope", DisplayName = "Display Name" } },
                Subject = new SimpleClaimsPrincipal
                {
                    Identities = new List<SimpleClaimsIdentity> { new SimpleClaimsIdentity { Claims = new List<SimpleClaim>() } }
                },
                CodeChallenge = "CodeChallenge",
                CodeChallengeMethod = "CodeChallengeMethod",
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                IsOpenId = true,
                Nonce = "Nonce",
                RedirectUri = "RedirectUri",
                SessionId = "SessionId",
                WasConsentShown = true
            };

            var jsonSettingsFactory = new JsonSettingsFactory(new CustomMappersConfiguration()).Create();

            var serializeObject = JsonConvert.SerializeObject(code, jsonSettingsFactory);

            Console.WriteLine($"{Encoding.UTF8.GetByteCount(serializeObject.Compress())} bytes");
        }

        /// <summary>
        /// Tests the sizes no compress.
        /// </summary>
        [Test]
        public void TestSizesNoCompress()
        {
            var code = new SimpleAuthorizationCode
            {
                Client = new SimpleClient
                {
                    ClientId = "cid"
                },
                RequestedScopes = new List<SimpleScope> { new SimpleScope { Description = "this is description", Enabled = true, Name = "Scope", DisplayName = "Display Name" } },
                Subject = new SimpleClaimsPrincipal
                {
                    Identities = new List<SimpleClaimsIdentity> { new SimpleClaimsIdentity { Claims = new List<SimpleClaim>() } }
                },
                CodeChallenge = "CodeChallenge",
                CodeChallengeMethod = "CodeChallengeMethod",
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                IsOpenId = true,
                Nonce = "Nonce",
                RedirectUri = "RedirectUri",
                SessionId = "SessionId",
                WasConsentShown = true
            };

            var jsonSettingsFactory = new JsonSettingsFactory(new CustomMappersConfiguration()).Create();

            var serializeObject = JsonConvert.SerializeObject(code, jsonSettingsFactory);

            Console.WriteLine($"{Encoding.UTF8.GetByteCount(serializeObject)} bytes");
        }
    }
}