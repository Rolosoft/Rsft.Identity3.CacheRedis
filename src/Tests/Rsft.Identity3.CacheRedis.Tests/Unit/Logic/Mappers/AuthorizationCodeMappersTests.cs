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

// <copyright file="AuthorizationCodeMappersTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Unit.Logic.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
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
    /// The Authorization Code Mappers Tests
    /// </summary>
    /// <seealso cref="TestBase" />
    [TestFixture]
    public sealed class AuthorizationCodeMappersTests : TestBase
    {
        /// <summary>
        /// To the complex entity when simple entity expect correct map.
        /// </summary>
        [Test]
        public void ToComplexEntity_WhenSimpleEntity_ExpectCorrectMap()
        {
            // Arrange
            var mockPropertyMapper = new Mock<IPropertyGetSettersTyped<AuthorizationCode>>();
            var mockClaimsPrincipalMapper = new Mock<IMapper<SimpleClaimsPrincipal, ClaimsPrincipal>>();
            var mockClientMapper = new Mock<IMapper<SimpleClient, Client>>();
            var mockScopeMapper = new Mock<IMapper<SimpleScope, Scope>>();

            mockClaimsPrincipalMapper.Setup(r => r.ToComplexEntity(It.IsAny<SimpleClaimsPrincipal>()))
                .Returns(new ClaimsPrincipal());

            mockPropertyMapper.Setup(r => r.GetSetters(It.IsAny<Type>())).Returns(new Dictionary<string, TypedSetter<AuthorizationCode>>());

            mockClientMapper.Setup(r => r.ToComplexEntity(It.IsAny<SimpleClient>())).Returns(new Client());
            mockScopeMapper.Setup(r => r.ToComplexEntity(It.IsAny<IEnumerable<SimpleScope>>())).Returns(new List<Scope> { new Scope() });

            var authorizationCodeMappers = new AuthorizationCodeMappers<AuthorizationCode>(
                mockPropertyMapper.Object,
                mockClaimsPrincipalMapper.Object,
                mockClientMapper.Object,
                mockScopeMapper.Object);

            var simpleEntity = new SimpleAuthorizationCode
            {
                Client = new SimpleClient(),
                Subject = new SimpleClaimsPrincipal(),
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                RedirectUri = "RedirectUri",
                Nonce = "Nonce",
                WasConsentShown = true,
                CodeChallengeMethod = "CodeChallengeMethod",
                IsOpenId = true,
                SessionId = "SessionId",
                CodeChallenge = "CodeChallenge",
                RequestedScopes = new List<SimpleScope>()
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var complexEntity = authorizationCodeMappers.ToComplexEntity(simpleEntity);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(complexEntity, Is.Not.Null);

            Assert.That(complexEntity.Client, Is.Not.Null);
            Assert.That(complexEntity.Subject, Is.Not.Null);
            Assert.That(complexEntity.CreationTime, Is.EqualTo(new DateTimeOffset(new DateTime(2016, 1, 1))));
            Assert.That(complexEntity.RedirectUri, Is.EqualTo("RedirectUri"));
            Assert.That(complexEntity.Nonce, Is.EqualTo("Nonce"));
            Assert.That(complexEntity.WasConsentShown, Is.True);
            Assert.That(complexEntity.CodeChallengeMethod, Is.EqualTo("CodeChallengeMethod"));
            Assert.That(complexEntity.IsOpenId, Is.True);
            Assert.That(complexEntity.SessionId, Is.EqualTo("SessionId"));
            Assert.That(complexEntity.CodeChallenge, Is.EqualTo("CodeChallenge"));
            Assert.That(complexEntity.RequestedScopes, Is.Not.Null);
            Assert.That(complexEntity.RequestedScopes.Any(), Is.True);
        }

        /// <summary>
        /// To the complex entity when simple entity and extended complex expect correct map.
        /// </summary>
        [Test]
        public void ToComplexEntity_WhenSimpleEntityAndExtendedComplex_ExpectCorrectMap()
        {
            // Arrange
            var mockPropertyMapper = new Mock<IPropertyGetSettersTyped<ExtendedAuthorizationCode>>();
            var mockClaimsPrincipalMapper = new Mock<IMapper<SimpleClaimsPrincipal, ClaimsPrincipal>>();
            var mockClientMapper = new Mock<IMapper<SimpleClient, Client>>();
            var mockScopeMapper = new Mock<IMapper<SimpleScope, Scope>>();

            mockClaimsPrincipalMapper.Setup(r => r.ToComplexEntity(It.IsAny<SimpleClaimsPrincipal>()))
                .Returns(new ClaimsPrincipal());

            var typedSetter = new TypedSetter<ExtendedAuthorizationCode>
            {
                OriginalType = typeof(int),
                Setter = typeof(ExtendedAuthorizationCode).GetSetter<ExtendedAuthorizationCode>("CustomNumber")
            };

            mockPropertyMapper.Setup(r => r.GetSetters(It.IsAny<Type>()))
                .Returns(new Dictionary<string, TypedSetter<ExtendedAuthorizationCode>> { { "CustomNumber", typedSetter } });

            mockClientMapper.Setup(r => r.ToComplexEntity(It.IsAny<SimpleClient>())).Returns(new Client());
            mockScopeMapper.Setup(r => r.ToComplexEntity(It.IsAny<IEnumerable<SimpleScope>>())).Returns(new List<Scope> { new Scope() });

            var authorizationCodeMappers = new AuthorizationCodeMappers<ExtendedAuthorizationCode>(
                mockPropertyMapper.Object,
                mockClaimsPrincipalMapper.Object,
                mockClientMapper.Object,
                mockScopeMapper.Object);

            var simpleEntity = new SimpleAuthorizationCode
            {
                Client = new SimpleClient(),
                Subject = new SimpleClaimsPrincipal(),
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                RedirectUri = "RedirectUri",
                Nonce = "Nonce",
                WasConsentShown = true,
                CodeChallengeMethod = "CodeChallengeMethod",
                IsOpenId = true,
                SessionId = "SessionId",
                CodeChallenge = "CodeChallenge",
                RequestedScopes = new List<SimpleScope>(),
                DataBag = new Dictionary<string, object> { { "CustomNumber", 12 } }
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var complexEntity = authorizationCodeMappers.ToComplexEntity(simpleEntity);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(complexEntity, Is.Not.Null);

            Assert.That(complexEntity.Client, Is.Not.Null);
            Assert.That(complexEntity.Subject, Is.Not.Null);
            Assert.That(complexEntity.CreationTime, Is.EqualTo(new DateTimeOffset(new DateTime(2016, 1, 1))));
            Assert.That(complexEntity.RedirectUri, Is.EqualTo("RedirectUri"));
            Assert.That(complexEntity.Nonce, Is.EqualTo("Nonce"));
            Assert.That(complexEntity.WasConsentShown, Is.True);
            Assert.That(complexEntity.CodeChallengeMethod, Is.EqualTo("CodeChallengeMethod"));
            Assert.That(complexEntity.IsOpenId, Is.True);
            Assert.That(complexEntity.SessionId, Is.EqualTo("SessionId"));
            Assert.That(complexEntity.CodeChallenge, Is.EqualTo("CodeChallenge"));
            Assert.That(complexEntity.RequestedScopes, Is.Not.Null);
            Assert.That(complexEntity.RequestedScopes.Any(), Is.True);
            Assert.That(complexEntity.CustomNumber, Is.EqualTo(12));
        }

        /// <summary>
        /// To the simple entity when complex entity expect correct map.
        /// </summary>
        [Test]
        public void ToSimpleEntity_WhenComplexEntityExtendedComplex_ExpectCorrectMap()
        {
            // Arrange
            var mockPropertyMapper = new Mock<IPropertyGetSettersTyped<ExtendedAuthorizationCode>>();
            var mockClaimsPrincipalMapper = new Mock<IMapper<SimpleClaimsPrincipal, ClaimsPrincipal>>();
            var mockClientMapper = new Mock<IMapper<SimpleClient, Client>>();
            var mockScopeMapper = new Mock<IMapper<SimpleScope, Scope>>();

            mockClaimsPrincipalMapper.Setup(r => r.ToSimpleEntity(It.IsAny<ClaimsPrincipal>()))
                .Returns(new SimpleClaimsPrincipal());

            var mockGetters = new Dictionary<string, Func<ExtendedAuthorizationCode, object>>
            {
                {
                    "CustomNumber",
                    typeof(ExtendedAuthorizationCode).GetProperty("CustomNumber").GetGetter<ExtendedAuthorizationCode>()
                }
            };

            mockPropertyMapper.Setup(r => r.GetGetters(It.IsAny<Type>())).Returns(mockGetters);

            mockClientMapper.Setup(r => r.ToSimpleEntity(It.IsAny<Client>())).Returns(new SimpleClient());
            mockScopeMapper.Setup(r => r.ToSimpleEntity(It.IsAny<IEnumerable<Scope>>())).Returns(new List<SimpleScope> { new SimpleScope() });

            var authorizationCodeMappers = new AuthorizationCodeMappers<ExtendedAuthorizationCode>(
                mockPropertyMapper.Object,
                mockClaimsPrincipalMapper.Object,
                mockClientMapper.Object,
                mockScopeMapper.Object);

            var complexEntity = new ExtendedAuthorizationCode
            {
                Client = new Client(),
                Subject = new ClaimsPrincipal(),
                CreationTime = new DateTimeOffset(new DateTime(2016, 1, 1)),
                RedirectUri = "RedirectUri",
                Nonce = "Nonce",
                WasConsentShown = true,
                CodeChallengeMethod = "CodeChallengeMethod",
                IsOpenId = true,
                SessionId = "SessionId",
                CodeChallenge = "CodeChallenge",
                RequestedScopes = new List<Scope>(),
                CustomNumber = 12
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var simpleEntity = authorizationCodeMappers.ToSimpleEntity(complexEntity);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(simpleEntity, Is.Not.Null);

            Assert.That(simpleEntity.Client, Is.Not.Null);
            Assert.That(simpleEntity.Subject, Is.Not.Null);
            Assert.That(simpleEntity.CreationTime, Is.EqualTo(new DateTimeOffset(new DateTime(2016, 1, 1))));
            Assert.That(simpleEntity.RedirectUri, Is.EqualTo("RedirectUri"));
            Assert.That(simpleEntity.Nonce, Is.EqualTo("Nonce"));
            Assert.That(simpleEntity.WasConsentShown, Is.True);
            Assert.That(simpleEntity.CodeChallengeMethod, Is.EqualTo("CodeChallengeMethod"));
            Assert.That(simpleEntity.IsOpenId, Is.True);
            Assert.That(simpleEntity.SessionId, Is.EqualTo("SessionId"));
            Assert.That(simpleEntity.CodeChallenge, Is.EqualTo("CodeChallenge"));
            Assert.That(simpleEntity.RequestedScopes, Is.Not.Null);
            Assert.That(simpleEntity.RequestedScopes.Any(), Is.True);
            Assert.That(simpleEntity.DataBag["CustomNumber"], Is.EqualTo(12));
        }
    }
}