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
    using System.Diagnostics.Contracts;
    using System.Security.Claims;
    using Entities.Serialization;
    using Interfaces;

    /// <summary>
    /// The Claims Identity Mappers
    /// </summary>
    internal sealed class ClaimsIdentityMappers : BaseMapper<SimpleClaimsIdentity, ClaimsIdentity>
    {
        /// <summary>
        /// The claim mapper
        /// </summary>
        private readonly IMapper<SimpleClaim, Claim> claimMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimsIdentityMappers"/> class.
        /// </summary>
        /// <param name="claimMapper">The claim mapper.</param>
        public ClaimsIdentityMappers(IMapper<SimpleClaim, Claim> claimMapper)
        {
            Contract.Requires(claimMapper != null);

            this.claimMapper = claimMapper;
        }

        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="ClaimsIdentity"/></returns>
        public override ClaimsIdentity ToComplexEntity(SimpleClaimsIdentity source)
        {
            if (source == null)
            {
                return null;
            }

            var claims = this.claimMapper.ToComplexEntity(source.Claims);
            //     var actor = this.ToComplexEntity(source.Actor);

            return new ClaimsIdentity(claims, source.AuthenticationType, source.NameClaimType, source.RoleClaimType)
            {
                //       Actor = actor,
                BootstrapContext = source.BootstrapContext,
                Label = source.Label
            };
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="SimpleClaimsIdentity"/></returns>
        public override SimpleClaimsIdentity ToSimpleEntity(ClaimsIdentity source)
        {
            if (source == null)
            {
                return null;
            }

            var claims = this.claimMapper.ToSimpleEntity(source.Claims);
            //      var actor = this.ToSimpleEntity(source.Actor);

            return new SimpleClaimsIdentity
            {
                Claims = claims,
                //       Actor = actor,
                AuthenticationType = source.AuthenticationType
            };
        }
    }
}