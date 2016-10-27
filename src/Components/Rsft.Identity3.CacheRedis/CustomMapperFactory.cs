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

// <copyright file="CustomMapperFactory.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis
{
    using System.Security.Claims;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces;
    using Interfaces.Serialization;
    using Logic.Mappers;
    using Logic.Serialization;

    /// <summary>
    /// The Custom Mapper Factory
    /// </summary>
    public static class CustomMapperFactory
    {
        /// <summary>
        /// The property information store
        /// </summary>
        private static readonly PropertyInfoStore PropertyInfoStore = new PropertyInfoStore();

        /// <summary>
        /// Initializes static members of the <see cref="CustomMapperFactory"/> class.
        /// </summary>
        static CustomMapperFactory()
        {
            ClaimMapper = new ClaimMappers();
            ClaimsIdentityMapper = new ClaimsIdentityMappers(ClaimMapper);
            ClaimsPrincipalMapper = new ClaimsPrincipalMappers(ClaimsIdentityMapper);
        }

        /// <summary>
        /// Gets the get claim mapper.
        /// </summary>
        /// <value>
        /// The claim mapper.
        /// </value>
        internal static IMapper<SimpleClaim, Claim> ClaimMapper { get; }

        /// <summary>
        /// Gets the claims identity mapper.
        /// </summary>
        /// <value>
        /// The claims identity mapper.
        /// </value>
        internal static IMapper<SimpleClaimsIdentity, ClaimsIdentity> ClaimsIdentityMapper { get; }

        /// <summary>
        /// Gets the claims principal mapper.
        /// </summary>
        /// <value>
        /// The claims principal mapper.
        /// </value>
        internal static IMapper<SimpleClaimsPrincipal, ClaimsPrincipal> ClaimsPrincipalMapper { get; }

        /// <summary>
        /// Creates the authorization code mapper.
        /// </summary>
        /// <typeparam name="TAuthorizationCode">The type of the authorization code.</typeparam>
        /// <param name="clientMapper">The client mapper.</param>
        /// <param name="scopeMapper">The scope mapper.</param>
        /// <returns>
        /// The <see cref="IMapper{T,T}" />
        /// </returns>
        public static IMapper<SimpleAuthorizationCode, TAuthorizationCode> CreateAuthorizationCodeMapper
            <TAuthorizationCode>(
            IMapper<SimpleClient, Client> clientMapper = null,
            IMapper<SimpleScope, Scope> scopeMapper = null)
            where TAuthorizationCode : AuthorizationCode, new()
        {
            IPropertyGetSettersTyped<TAuthorizationCode> propertyMapper = new PropertyGetSettersTyped<TAuthorizationCode>(PropertyInfoStore);

            var myClientMapper = clientMapper ?? new ClientMappers<Client>(ClaimMapper, new PropertyGetSettersTyped<Client>(PropertyInfoStore));

            var rtn = new AuthorizationCodeMappers<TAuthorizationCode>(propertyMapper, ClaimsPrincipalMapper, myClientMapper, scopeMapper);

            return rtn;
        }

        /// <summary>
        /// Creates the client mapper.
        /// </summary>
        /// <typeparam name="TClient">The type of the client.</typeparam>
        /// <returns>The <see cref="IMapper{T,T}"/></returns>
        public static IMapper<SimpleClient, TClient> CreateClientMapper<TClient>()
            where TClient : Client, new()
        {
            var propertyMapper = new PropertyGetSettersTyped<TClient>(PropertyInfoStore);

            return new ClientMappers<TClient>(ClaimMapper, propertyMapper);
        }

        /// <summary>
        /// Creates the refresh token mapper.
        /// </summary>
        /// <typeparam name="TRefreshToken">The type of the refresh token.</typeparam>
        /// <param name="tokenMapper">The token mapper.</param>
        /// <returns>
        /// The <see cref="IMapper{T,T}" />
        /// </returns>
        public static IMapper<SimpleRefreshToken, TRefreshToken> CreateRefreshTokenMapper<TRefreshToken>(
            IMapper<SimpleToken, Token> tokenMapper = null)
            where TRefreshToken : RefreshToken, new()
        {
            var propertyMapper = new PropertyGetSettersTyped<TRefreshToken>(PropertyInfoStore);

            var myTokenMapper = tokenMapper ?? CreateTokenMapper<Token>();

            return new RefreshTokenMappers<TRefreshToken>(propertyMapper, ClaimsPrincipalMapper, myTokenMapper);
        }

        /// <summary>
        /// Creates the scope mapper.
        /// </summary>
        /// <typeparam name="TScope">The type of the scope.</typeparam>
        /// <returns>The <see cref="IMapper{T,T}"/></returns>
        public static IMapper<SimpleScope, TScope> CreateScopeMapper<TScope>()
            where TScope : Scope, new()
        {
            var propertyMapper = new PropertyGetSettersTyped<TScope>(PropertyInfoStore);

            return new ScopeMappers<TScope>(propertyMapper);
        }

        /// <summary>
        /// Creates the token mapper.
        /// </summary>
        /// <typeparam name="TToken">The type of the token.</typeparam>
        /// <param name="clientMapper">The client mapper.</param>
        /// <returns>
        /// The <see cref="IMapper{T,T}" />
        /// </returns>
        public static IMapper<SimpleToken, TToken> CreateTokenMapper<TToken>(
            IMapper<SimpleClient, Client> clientMapper = null)
            where TToken : Token, new()
        {
            var propertyMapper = new PropertyGetSettersTyped<TToken>(PropertyInfoStore);

            var myClientMapper = clientMapper ?? CreateClientMapper<Client>();

            return new TokenMapper<TToken>(propertyMapper, ClaimMapper, myClientMapper);
        }
    }
}