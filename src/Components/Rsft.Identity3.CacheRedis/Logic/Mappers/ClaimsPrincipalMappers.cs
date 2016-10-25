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

// <copyright file="ClaimsPrincipalMappers.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Mappers
{
    using System.Diagnostics.Contracts;
    using System.Security.Claims;
    using Entities.Serialization;
    using Interfaces;

    /// <summary>
    /// The Claims Principal Mappers
    /// </summary>
    internal sealed class ClaimsPrincipalMappers : BaseMapper<SimpleClaimsPrincipal, ClaimsPrincipal>
    {
        /// <summary>
        /// The claims mapper
        /// </summary>
        private readonly IMapper<SimpleClaimsIdentity, ClaimsIdentity> claimsIdentityMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimsPrincipalMappers" /> class.
        /// </summary>
        /// <param name="claimsIdentityMapper">The claims identity mapper.</param>
        public ClaimsPrincipalMappers(IMapper<SimpleClaimsIdentity, ClaimsIdentity> claimsIdentityMapper)
        {
            Contract.Requires(claimsIdentityMapper != null);

            this.claimsIdentityMapper = claimsIdentityMapper;
        }

        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="ClaimsPrincipal"/></returns>
        public override ClaimsPrincipal ToComplexEntity(SimpleClaimsPrincipal source)
        {
            if (source == null)
            {
                return null;
            }

            var identities = this.claimsIdentityMapper.ToComplexEntity(source.Identities);

            return new ClaimsPrincipal(identities);
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="SimpleClaimsPrincipal"/></returns>
        public override SimpleClaimsPrincipal ToSimpleEntity(ClaimsPrincipal source)
        {
            if (source == null)
            {
                return null;
            }

            var identities = this.claimsIdentityMapper.ToSimpleEntity(source.Identities);

            return new SimpleClaimsPrincipal
            {
                Identities = identities
            };
        }
    }
}