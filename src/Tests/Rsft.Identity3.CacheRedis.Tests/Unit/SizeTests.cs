// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SizeTests.cs" company="Email Hippo Ltd">
//   © Email Hippo Ltd
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Rsft.Identity3.CacheRedis.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CacheRedis.Logic;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Util.Compression;

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

            var jsonSettingsFactory = new JsonSettingsFactory(new ClientMapperBase<Client>()).Create();

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

            var jsonSettingsFactory = new JsonSettingsFactory(new ClientMapperBase<Client>()).Create();

            var serializeObject = JsonConvert.SerializeObject(code, jsonSettingsFactory);

            Console.WriteLine($"{Encoding.UTF8.GetByteCount(serializeObject)} bytes");
        }
    }
}