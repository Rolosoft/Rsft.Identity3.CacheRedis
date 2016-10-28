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

// <copyright file="ScopeMappersTests.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Unit.Logic.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using CacheRedis.Logic.Mappers;
    using Entities;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using InheritedEntities;
    using Interfaces.Serialization;
    using Moq;
    using NUnit.Framework;
    using TestHelpers;

    /// <summary>
    /// The Scope Mappers Tests
    /// </summary>
    [TestFixture]
    public sealed class ScopeMappersTests : TestBase
    {
        /// <summary>
        /// To the complex entity when simple entity expect map success.
        /// </summary>
        [Test]
        public void ToComplexEntity_WhenSimpleEntity_ExpectMapSuccess()
        {
            // Arrange
            var mockPropertyMapper = new Mock<IPropertyGetSettersTyped<Scope>>();

            mockPropertyMapper.Setup(r => r.GetSetters(It.IsAny<Type>()))
                .Returns(new Dictionary<string, TypedSetter<Scope>>());

            var scopeMappers = new ScopeMappers<Scope>(mockPropertyMapper.Object);

            var scopeClaim = new ScopeClaim("Name", true) { Description = "Description" };
            var secret = new Secret("Value", "Description", new DateTimeOffset(new DateTime(2016, 1, 1))) { Type = "Type" };

            var simpleScope = new SimpleScope
            {
                Claims = new List<ScopeClaim> { scopeClaim },
                Type = ScopeType.Identity,
                Enabled = true,
                AllowUnrestrictedIntrospection = true,
                ScopeSecrets = new List<Secret> { secret },
                DisplayName = "DisplayName",
                Emphasize = true,
                ClaimsRule = "ClaimsRule",
                IncludeAllClaimsForUser = true,
                Name = "Name",
                Required = true,
                ShowInDiscoveryDocument = true,
                Description = "Description"
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var complexEntity = scopeMappers.ToComplexEntity(simpleScope);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(complexEntity, Is.Not.Null);

            Assert.That(complexEntity.Claims, Is.Not.Null);
            Assert.That(complexEntity.Claims.Count, Is.EqualTo(1));

            Assert.That(complexEntity.Type, Is.EqualTo(ScopeType.Identity));
            Assert.That(complexEntity.Enabled, Is.True);
            Assert.That(complexEntity.AllowUnrestrictedIntrospection, Is.True);

            Assert.That(complexEntity.ScopeSecrets, Is.Not.Null);
            Assert.That(complexEntity.ScopeSecrets.Count, Is.EqualTo(1));

            Assert.That(complexEntity.DisplayName, Is.EqualTo("DisplayName"));
            Assert.That(complexEntity.Emphasize, Is.True);
            Assert.That(complexEntity.ClaimsRule, Is.EqualTo("ClaimsRule"));
            Assert.That(complexEntity.IncludeAllClaimsForUser, Is.True);
            Assert.That(complexEntity.Name, Is.EqualTo("Name"));
            Assert.That(complexEntity.Required, Is.True);
            Assert.That(complexEntity.ShowInDiscoveryDocument, Is.True);
            Assert.That(complexEntity.Description, Is.EqualTo("Description"));
        }

        /// <summary>
        /// To the complex entity when simple entity and extended complex expect map success.
        /// </summary>
        [Test]
        public void ToComplexEntity_WhenSimpleEntityAndExtendedComplex_ExpectMapSuccess()
        {
            // Arrange
            var mockPropertyMapper = new Mock<IPropertyGetSettersTyped<ExtendedScope>>();

            var typedSetter = new TypedSetter<ExtendedScope>
            {
                OriginalType = typeof(string),
                Setter = typeof(ExtendedScope).GetSetter<ExtendedScope>("CustomString")
            };

            mockPropertyMapper.Setup(r => r.GetSetters(It.IsAny<Type>()))
                .Returns(new Dictionary<string, TypedSetter<ExtendedScope>> { { "CustomString", typedSetter } });

            var scopeMappers = new ScopeMappers<ExtendedScope>(mockPropertyMapper.Object);

            var scopeClaim = new ScopeClaim("Name", true) { Description = "Description" };
            var secret = new Secret("Value", "Description", new DateTimeOffset(new DateTime(2016, 1, 1))) { Type = "Type" };

            var simpleScope = new SimpleScope
            {
                Claims = new List<ScopeClaim> { scopeClaim },
                Type = ScopeType.Identity,
                Enabled = true,
                AllowUnrestrictedIntrospection = true,
                ScopeSecrets = new List<Secret> { secret },
                DisplayName = "DisplayName",
                Emphasize = true,
                ClaimsRule = "ClaimsRule",
                IncludeAllClaimsForUser = true,
                Name = "Name",
                Required = true,
                ShowInDiscoveryDocument = true,
                Description = "Description",
                DataBag = new Dictionary<string, object> { { "CustomString", "customString" } }
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var complexEntity = scopeMappers.ToComplexEntity(simpleScope);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(complexEntity, Is.Not.Null);

            Assert.That(complexEntity.Claims, Is.Not.Null);
            Assert.That(complexEntity.Claims.Count, Is.EqualTo(1));

            Assert.That(complexEntity.Type, Is.EqualTo(ScopeType.Identity));
            Assert.That(complexEntity.Enabled, Is.True);
            Assert.That(complexEntity.AllowUnrestrictedIntrospection, Is.True);

            Assert.That(complexEntity.ScopeSecrets, Is.Not.Null);
            Assert.That(complexEntity.ScopeSecrets.Count, Is.EqualTo(1));

            Assert.That(complexEntity.DisplayName, Is.EqualTo("DisplayName"));
            Assert.That(complexEntity.Emphasize, Is.True);
            Assert.That(complexEntity.ClaimsRule, Is.EqualTo("ClaimsRule"));
            Assert.That(complexEntity.IncludeAllClaimsForUser, Is.True);
            Assert.That(complexEntity.Name, Is.EqualTo("Name"));
            Assert.That(complexEntity.Required, Is.True);
            Assert.That(complexEntity.ShowInDiscoveryDocument, Is.True);
            Assert.That(complexEntity.Description, Is.EqualTo("Description"));
            Assert.That(complexEntity.CustomString, Is.EqualTo("customString"));
        }

        /// <summary>
        /// To the simple entity when complex entity expect map success.
        /// </summary>
        [Test]
        public void ToSimpleEntity_WhenComplexEntity_ExpectMapSuccess()
        {
            // Arrange#
            var mockPropertyMapper = new Mock<IPropertyGetSettersTyped<Scope>>();

            mockPropertyMapper.Setup(r => r.GetGetters(It.IsAny<Type>()))
                .Returns(new Dictionary<string, Func<Scope, object>>());

            var scopeMappers = new ScopeMappers<Scope>(mockPropertyMapper.Object);

            var scopeClaim = new ScopeClaim("Name", true) { Description = "Description" };
            var secret = new Secret("Value", "Description", new DateTimeOffset(new DateTime(2016, 1, 1))) { Type = "Type" };

            var scope = new Scope
            {
                Claims = new List<ScopeClaim> { scopeClaim },
                Type = ScopeType.Identity,
                Enabled = true,
                AllowUnrestrictedIntrospection = true,
                ScopeSecrets = new List<Secret> { secret },
                DisplayName = "DisplayName",
                Emphasize = true,
                ClaimsRule = "ClaimsRule",
                IncludeAllClaimsForUser = true,
                Name = "Name",
                Required = true,
                ShowInDiscoveryDocument = true,
                Description = "Description"
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var simpleEntity = scopeMappers.ToSimpleEntity(scope);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(simpleEntity, Is.Not.Null);

            Assert.That(simpleEntity.Claims, Is.Not.Null);
            Assert.That(simpleEntity.Claims.Count, Is.EqualTo(1));

            Assert.That(simpleEntity.Type, Is.EqualTo(ScopeType.Identity));
            Assert.That(simpleEntity.Enabled, Is.True);
            Assert.That(simpleEntity.AllowUnrestrictedIntrospection, Is.True);

            Assert.That(simpleEntity.ScopeSecrets, Is.Not.Null);
            Assert.That(simpleEntity.ScopeSecrets.Count, Is.EqualTo(1));

            Assert.That(simpleEntity.DisplayName, Is.EqualTo("DisplayName"));
            Assert.That(simpleEntity.Emphasize, Is.True);
            Assert.That(simpleEntity.ClaimsRule, Is.EqualTo("ClaimsRule"));
            Assert.That(simpleEntity.IncludeAllClaimsForUser, Is.True);
            Assert.That(simpleEntity.Name, Is.EqualTo("Name"));
            Assert.That(simpleEntity.Required, Is.True);
            Assert.That(simpleEntity.ShowInDiscoveryDocument, Is.True);
            Assert.That(simpleEntity.Description, Is.EqualTo("Description"));
        }

        /// <summary>
        /// To the simple entity when complex entity and extended complex expect map success.
        /// </summary>
        [Test]
        public void ToSimpleEntity_WhenComplexEntityAndExtendedComplex_ExpectMapSuccess()
        {
            // Arrange#
            var mockPropertyMapper = new Mock<IPropertyGetSettersTyped<ExtendedScope>>();

            var mockGetters = new Dictionary<string, Func<ExtendedScope, object>>
            {
                {
                    "CustomString",
                    typeof(ExtendedScope).GetProperty("CustomString").GetGetter<ExtendedScope>()
                }
            };

            mockPropertyMapper.Setup(r => r.GetGetters(It.IsAny<Type>()))
                .Returns(mockGetters);

            var scopeMappers = new ScopeMappers<ExtendedScope>(mockPropertyMapper.Object);

            var scopeClaim = new ScopeClaim("Name", true) { Description = "Description" };
            var secret = new Secret("Value", "Description", new DateTimeOffset(new DateTime(2016, 1, 1))) { Type = "Type" };

            var scope = new ExtendedScope
            {
                Claims = new List<ScopeClaim> { scopeClaim },
                Type = ScopeType.Identity,
                Enabled = true,
                AllowUnrestrictedIntrospection = true,
                ScopeSecrets = new List<Secret> { secret },
                DisplayName = "DisplayName",
                Emphasize = true,
                ClaimsRule = "ClaimsRule",
                IncludeAllClaimsForUser = true,
                Name = "Name",
                Required = true,
                ShowInDiscoveryDocument = true,
                Description = "Description",
                CustomString = "customString"
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var simpleEntity = scopeMappers.ToSimpleEntity(scope);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(simpleEntity, Is.Not.Null);

            Assert.That(simpleEntity.Claims, Is.Not.Null);
            Assert.That(simpleEntity.Claims.Count, Is.EqualTo(1));

            Assert.That(simpleEntity.Type, Is.EqualTo(ScopeType.Identity));
            Assert.That(simpleEntity.Enabled, Is.True);
            Assert.That(simpleEntity.AllowUnrestrictedIntrospection, Is.True);

            Assert.That(simpleEntity.ScopeSecrets, Is.Not.Null);
            Assert.That(simpleEntity.ScopeSecrets.Count, Is.EqualTo(1));

            Assert.That(simpleEntity.DisplayName, Is.EqualTo("DisplayName"));
            Assert.That(simpleEntity.Emphasize, Is.True);
            Assert.That(simpleEntity.ClaimsRule, Is.EqualTo("ClaimsRule"));
            Assert.That(simpleEntity.IncludeAllClaimsForUser, Is.True);
            Assert.That(simpleEntity.Name, Is.EqualTo("Name"));
            Assert.That(simpleEntity.Required, Is.True);
            Assert.That(simpleEntity.ShowInDiscoveryDocument, Is.True);
            Assert.That(simpleEntity.Description, Is.EqualTo("Description"));

            Assert.That(simpleEntity.DataBag["CustomString"], Is.EqualTo("customString"));
        }
    }
}