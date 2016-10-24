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
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Util.Compression;

    [TestFixture]
    public class SizeTests : TestBase
    {
        [Test]
        public void TestSizesCompress()
        {
            var claim1 = new SimpleClaim { Type = "claim1", Value = "test@emailippo.com" };
            var claim2 = new SimpleClaim { Type = "claim2", Value = "simon@emailhippo.com" };
            var code = new SimpleAuthorizationCode
            {
                Client = new SimpleClient
                {
                    ClientId = "cid"
                },
                RequestedScopes = new List<SimpleScope> { new SimpleScope { Description = "this is description", Enabled = true, Name = "Scope", DisplayName = "Display Name" } },
                Subject = new SimpleClaimsPrincipal
                {
                    Claims = new List<SimpleClaim> { claim1, claim2 },
                    Identities = new List<SimpleClaimsIdentity> { new SimpleClaimsIdentity { Claims = new List<SimpleClaim>() } }
                },
            };

            var jsonSettingsFactory = new JsonSettingsFactory().Create(true);

            var serializeObject = JsonConvert.SerializeObject(code, jsonSettingsFactory);

            Console.WriteLine(Encoding.UTF8.GetByteCount(serializeObject.Compress()));
        }

        [Test]
        public void TestSizesNoCompress()
        {
            var claim1 = new SimpleClaim { Type = "claim1", Value = "test@emailippo.com" };
            var claim2 = new SimpleClaim { Type = "claim2", Value = "simon@emailhippo.com" };
            var code = new SimpleAuthorizationCode
            {
                Client = new SimpleClient
                {
                    ClientId = "cid"
                },
                RequestedScopes = new List<SimpleScope> { new SimpleScope { Description = "this is description", Enabled = true, Name = "Scope", DisplayName = "Display Name" } },
                Subject = new SimpleClaimsPrincipal
                {
                    Claims = new List<SimpleClaim> { claim1, claim2 },
                    Identities = new List<SimpleClaimsIdentity> { new SimpleClaimsIdentity { Claims = new List<SimpleClaim>() } }
                },
            };

            var jsonSettingsFactory = new JsonSettingsFactory().Create(false);

            var serializeObject = JsonConvert.SerializeObject(code, jsonSettingsFactory);

            Console.WriteLine(Encoding.UTF8.GetByteCount(serializeObject));
        }
    }
}