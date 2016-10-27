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
    using Interfaces.Serialization;

    /// <summary>
    /// The Token Mapper
    /// </summary>
    /// <typeparam name="TToken">The type of the token.</typeparam>
    /// <seealso cref="GenericMapper{SimpleToken, TToken}" />
    internal sealed class TokenMapper<TToken> : GenericMapper<SimpleToken, TToken>
        where TToken : Token, new()
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
        /// Initializes a new instance of the <see cref="TokenMapper{TToken}"/> class.
        /// </summary>
        /// <param name="propertyMapper">The property mapper.</param>
        /// <param name="claimsMapper">The claims mapper.</param>
        /// <param name="clientMapper">The client mapper.</param>
        public TokenMapper(
            IPropertyGetSettersTyped<TToken> propertyMapper,
            IMapper<SimpleClaim, Claim> claimsMapper,
            IMapper<SimpleClient, Client> clientMapper)
            : base(propertyMapper)
        {
            Contract.Requires(propertyMapper != null);
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
        public override TToken ToComplexEntity(SimpleToken source)
        {
            if (source == null)
            {
                return null;
            }

            var claims = this.claimsMapper.ToComplexEntity(source.Claims);
            var client = this.clientMapper.ToComplexEntity(source.Client);

            var token = base.ToComplexEntity(source);

            token.Claims = claims.ToList();
            token.Client = client;
            token.Type = source.Type;
            token.CreationTime = source.CreationTime;
            token.Audience = source.Audience;
            token.Issuer = source.Issuer;
            token.Lifetime = source.Lifetime;
            token.Version = source.Version;

            return token;
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The TSimpleEntity
        /// </returns>
        public override SimpleToken ToSimpleEntity(object source)
        {
            if (source == null)
            {
                return null;
            }

            var tokenSource = (TToken)source;

            var claims = this.claimsMapper.ToSimpleEntity(tokenSource.Claims);
            var client = this.clientMapper.ToSimpleEntity(tokenSource.Client);

            var token = base.ToSimpleEntity(source);

            token.Claims = claims.ToList();
            token.Client = client;
            token.Type = tokenSource.Type;
            token.CreationTime = tokenSource.CreationTime;
            token.Version = tokenSource.Version;
            token.Issuer = tokenSource.Issuer;
            token.Lifetime = tokenSource.Lifetime;
            token.Audience = tokenSource.Audience;

            return token;
        }
    }
}