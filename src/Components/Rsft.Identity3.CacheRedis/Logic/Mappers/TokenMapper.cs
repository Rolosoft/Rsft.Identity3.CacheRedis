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

// <copyright file="TokenMapper.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Mappers
{
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Security.Claims;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces;

    /// <summary>
    /// The Token Mapper
    /// </summary>
    internal sealed class TokenMapper : BaseMapper<SimpleToken, Token>
    {
        /// <summary>
        /// The claims mapper
        /// </summary>
        private readonly IMapper<SimpleClaim, Claim> claimsMapper;

        /// <summary>
        /// The client mapper
        /// </summary>
        private readonly IMapper<SimpleClient, Client> clientMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenMapper" /> class.
        /// </summary>
        /// <param name="claimsMapper">The claims mapper.</param>
        /// <param name="clientMapper">The client mapper.</param>
        public TokenMapper(IMapper<SimpleClaim, Claim> claimsMapper, IMapper<SimpleClient, Client> clientMapper)
        {
            Contract.Requires(claimsMapper != null);
            Contract.Requires(clientMapper != null);

            this.claimsMapper = claimsMapper;
            this.clientMapper = clientMapper;
        }

        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The TComplexEntity
        /// </returns>
        public override Token ToComplexEntity(SimpleToken source)
        {
            if (source == null)
            {
                return null;
            }

            var claims = this.claimsMapper.ToComplexEntity(source.Claims);

            return new Token
            {
                Claims = claims.ToList(),
                Client = source.Client,
                Type = source.Type,
                CreationTime = source.CreationTime,
                Audience = source.Audience,
                Issuer = source.Issuer,
                Lifetime = source.Lifetime,
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
        public override SimpleToken ToSimpleEntity(Token source)
        {
            if (source == null)
            {
                return null;
            }

            var claims = this.claimsMapper.ToSimpleEntity(source.Claims);

            return new SimpleToken
            {
                Claims = claims.ToList(),
                Client = source.Client,
                Type = source.Type,
                CreationTime = source.CreationTime,
                Version = source.Version,
                Issuer = source.Issuer,
                Lifetime = source.Lifetime,
                Audience = source.Audience
            };
        }
    }
}