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

// <copyright file="ScopeMappers.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Mappers
{
    using System.Diagnostics.Contracts;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces.Serialization;

    /// <summary>
    /// The Scope Mappers
    /// </summary>
    /// <typeparam name="TScope">The type of the scope.</typeparam>
    /// <seealso cref="GenericMapper{SimpleScope, TScope}" />
    internal sealed class ScopeMappers<TScope> : GenericMapper<SimpleScope, TScope>
        where TScope : Scope, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScopeMappers{TScope}"/> class.
        /// </summary>
        /// <param name="propertyMapper">The property mapper.</param>
        public ScopeMappers(IPropertyGetSettersTyped<TScope> propertyMapper)
            : base(propertyMapper)
        {
            Contract.Requires(propertyMapper != null);
        }

        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="ToComplexEntity"/></returns>
        public override TScope ToComplexEntity(SimpleScope source)
        {
            if (source == null)
            {
                return null;
            }

            var scope = base.ToComplexEntity(source);

            scope.Claims = source.Claims;
            scope.Type = source.Type;
            scope.Enabled = source.Enabled;
            scope.AllowUnrestrictedIntrospection = source.AllowUnrestrictedIntrospection;
            scope.Required = source.Required;
            scope.Emphasize = source.Emphasize;
            scope.IncludeAllClaimsForUser = source.IncludeAllClaimsForUser;
            scope.ShowInDiscoveryDocument = source.ShowInDiscoveryDocument;
            scope.ScopeSecrets = source.ScopeSecrets;
            scope.ClaimsRule = source.ClaimsRule;
            scope.Name = source.Name;
            scope.Description = source.Description;
            scope.DisplayName = source.DisplayName;

            return scope;
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="SimpleScope"/></returns>
        public override SimpleScope ToSimpleEntity(object source)
        {
            if (source == null)
            {
                return null;
            }

            var scopeSource = (TScope)source;

            var scope = base.ToSimpleEntity(source);

            scope.Claims = scopeSource.Claims;
            scope.Enabled = scopeSource.Enabled;
            scope.Type = scopeSource.Type;
            scope.Name = scopeSource.Name;
            scope.AllowUnrestrictedIntrospection = scopeSource.AllowUnrestrictedIntrospection;
            scope.ClaimsRule = scopeSource.ClaimsRule;
            scope.Description = scopeSource.Description;
            scope.DisplayName = scopeSource.DisplayName;
            scope.Emphasize = scopeSource.Emphasize;
            scope.IncludeAllClaimsForUser = scopeSource.IncludeAllClaimsForUser;
            scope.Required = scopeSource.Required;
            scope.ScopeSecrets = scopeSource.ScopeSecrets;
            scope.ShowInDiscoveryDocument = scopeSource.ShowInDiscoveryDocument;

            return scope;
        }
    }
}