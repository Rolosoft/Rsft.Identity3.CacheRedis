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

// <copyright file="RefreshTokenMappers.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Mappers
{
    using System.Diagnostics.Contracts;
    using System.Security.Claims;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces;

    /// <summary>
    /// The Refresh Token Mappers
    /// </summary>
    internal sealed class RefreshTokenMappers : BaseMapper<SimpleRefreshToken, RefreshToken>
    {
        /// <summary>
        /// The claims principal mapper
        /// </summary>
        private readonly IMapper<SimpleClaimsPrincipal, ClaimsPrincipal> claimsPrincipalMapper;

        private readonly IMapper<SimpleToken, Token> tokenMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshTokenMappers" /> class.
        /// </summary>
        /// <param name="claimsPrincipalMapper">The claims principal mapper.</param>
        /// <param name="tokenMapper">The token mapper.</param>
        public RefreshTokenMappers(
            IMapper<SimpleClaimsPrincipal, ClaimsPrincipal> claimsPrincipalMapper,
            IMapper<SimpleToken, Token> tokenMapper)
        {
            Contract.Requires(claimsPrincipalMapper != null);
            Contract.Requires(tokenMapper != null);

            this.claimsPrincipalMapper = claimsPrincipalMapper;
            this.tokenMapper = tokenMapper;
        }

        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The TComplexEntity
        /// </returns>
        public override RefreshToken ToComplexEntity(SimpleRefreshToken source)
        {
            if (source == null)
            {
                return null;
            }

            var subject = this.claimsPrincipalMapper.ToComplexEntity(source.Subject);
            var token = this.tokenMapper.ToComplexEntity(source.AccessToken);

            return new RefreshToken
            {
                Subject = subject,
                CreationTime = source.CreationTime,
                AccessToken = token,
                LifeTime = source.LifeTime,
                Version = source.Version
            };
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The TSimpleEntity
        /// </returns>
        public override SimpleRefreshToken ToSimpleEntity(RefreshToken source)
        {
            if (source == null)
            {
                return null;
            }

            var subject = this.claimsPrincipalMapper.ToSimpleEntity(source.Subject);
            var token = this.tokenMapper.ToSimpleEntity(source.AccessToken);

            return new SimpleRefreshToken
            {
                Subject = subject,
                CreationTime = source.CreationTime,
                AccessToken = token,
                LifeTime = source.LifeTime,
                Version = source.Version
            };
        }
    }
}