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
    using Entities.Serialization;
    using IdentityServer3.Core.Models;

    /// <summary>
    /// The Scope Mappers
    /// </summary>
    internal sealed class ScopeMappers : BaseMapper<SimpleScope, Scope>
    {
        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="ToComplexEntity"/></returns>
        public override Scope ToComplexEntity(SimpleScope source)
        {
            if (source == null)
            {
                return null;
            }

            return new Scope
            {
                Claims = source.Claims,
                Type = source.Type,
                Enabled = source.Enabled,
                AllowUnrestrictedIntrospection = source.AllowUnrestrictedIntrospection,
                Required = source.Required,
                Emphasize = source.Emphasize,
                IncludeAllClaimsForUser = source.IncludeAllClaimsForUser,
                ShowInDiscoveryDocument = source.ShowInDiscoveryDocument,
                ScopeSecrets = source.ScopeSecrets,
                ClaimsRule = source.ClaimsRule,
                Name = source.Name,
                Description = source.Description,
                DisplayName = source.DisplayName
            };
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="SimpleScope"/></returns>
        public override SimpleScope ToSimpleEntity(Scope source)
        {
            if (source == null)
            {
                return null;
            }

            return new SimpleScope
            {
                Claims = source.Claims,
                Enabled = source.Enabled,
                Type = source.Type,
                Name = source.Name,
                AllowUnrestrictedIntrospection = source.AllowUnrestrictedIntrospection,
                ClaimsRule = source.ClaimsRule,
                Description = source.Description,
                DisplayName = source.DisplayName,
                Emphasize = source.Emphasize,
                IncludeAllClaimsForUser = source.IncludeAllClaimsForUser,
                Required = source.Required,
                ScopeSecrets = source.ScopeSecrets,
                ShowInDiscoveryDocument = source.ShowInDiscoveryDocument
            };
        }
    }
}