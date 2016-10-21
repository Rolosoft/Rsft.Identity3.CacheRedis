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

// <copyright file="ClaimsIdentityMappers.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Mappers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Entities.Serialization;

    /// <summary>
    /// The Claims Identity Mappers
    /// </summary>
    internal static class ClaimsIdentityMappers
    {
        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="ClaimsIdentity"/></returns>
        public static ClaimsIdentity ToComplexEntity(this SimpleClaimsIdentity source)
        {
            if (source == null)
            {
                return null;
            }

            return new ClaimsIdentity(source.Claims, source.AuthenticationType, source.NameClaimType, source.RoleClaimType)
            {
                Actor = source.Actor.ToComplexEntity(),
                Claims = { },
                BootstrapContext = source.BootstrapContext,
                Label = source.Label
            };
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="SimpleClaimsIdentity"/></returns>
        public static SimpleClaimsIdentity ToSimpleEntity(this ClaimsIdentity source)
        {
            if (source == null)
            {
                return null;
            }

            return new SimpleClaimsIdentity
            {
                Claims = source.Claims.ToSimpleEntity(),
                Actor = source.Actor.ToSimpleEntity(),
                AuthenticationType = source.AuthenticationType
            };
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="SimpleClaimsIdentity"/></returns>
        public static IEnumerable<SimpleClaimsIdentity> ToSimpleEntity(this IEnumerable<ClaimsIdentity> source)
        {
            return source?.Select(r => r.ToSimpleEntity());
        }
    }
}