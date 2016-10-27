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

// <copyright file="AuthorizationCodeMappers.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Mappers
{
    using System.Security.Claims;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces;
    using Interfaces.Serialization;

    /// <summary>
    /// Teh Authorization Code Mappers
    /// </summary>
    /// <typeparam name="TAuthorizationCode">The type of the authorization code.</typeparam>
    /// <seealso cref="GenericMapper{SimpleAuthorizationCode, TAuthorizationCode}" />
    internal sealed class AuthorizationCodeMappers<TAuthorizationCode> : GenericMapper<SimpleAuthorizationCode, TAuthorizationCode>
        where TAuthorizationCode : AuthorizationCode, new()
    {
        /// <summary>
        /// The claims principal mapper
        /// </summary>
        private readonly IMapper<SimpleClaimsPrincipal, ClaimsPrincipal> claimsPrincipalMapper;

        /// <summary>
        /// The client mapper
        /// </summary>
        private readonly IMapper<SimpleClient, Client> clientMapper;

        /// <summary>
        /// The scope mapper
        /// </summary>
        private readonly IMapper<SimpleScope, Scope> scopeMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationCodeMappers{TAuthorizationCode}" /> class.
        /// </summary>
        /// <param name="propertyMapper">The property mapper.</param>
        /// <param name="claimsPrincipalMapper">The claims principal mapper.</param>
        /// <param name="clientMapper">The client mapper.</param>
        /// <param name="scopeMapper">The scope mapper.</param>
        public AuthorizationCodeMappers(
            IPropertyGetSettersTyped<TAuthorizationCode> propertyMapper,
            IMapper<SimpleClaimsPrincipal, ClaimsPrincipal> claimsPrincipalMapper,
            IMapper<SimpleClient, Client> clientMapper,
            IMapper<SimpleScope, Scope> scopeMapper)
            : base(propertyMapper)
        {
            this.claimsPrincipalMapper = claimsPrincipalMapper;
            this.clientMapper = clientMapper;
            this.scopeMapper = scopeMapper;
        }

        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="AuthorizationCode"/></returns>
        public override TAuthorizationCode ToComplexEntity(SimpleAuthorizationCode source)
        {
            if (source == null)
            {
                return null;
            }

            var subject = this.claimsPrincipalMapper.ToComplexEntity(source.Subject);
            var client = this.clientMapper.ToComplexEntity(source.Client);
            var requestedScopes = this.scopeMapper.ToComplexEntity(source.RequestedScopes);

            var rtn = base.ToComplexEntity(source);

            rtn.Subject = subject;
            rtn.Client = client;
            rtn.Nonce = source.Nonce;
            rtn.RedirectUri = source.RedirectUri;
            rtn.CodeChallenge = source.CodeChallenge;
            rtn.CodeChallengeMethod = source.CodeChallengeMethod;
            rtn.CreationTime = source.CreationTime;
            rtn.IsOpenId = source.IsOpenId;
            rtn.RequestedScopes = requestedScopes;
            rtn.SessionId = source.SessionId;
            rtn.WasConsentShown = source.WasConsentShown;

            return rtn;
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="SimpleAuthorizationCode"/></returns>
        public override SimpleAuthorizationCode ToSimpleEntity(object source)
        {
            if (source == null)
            {
                return null;
            }

            var authorizationCodeSource = (TAuthorizationCode)source;

            var subject = this.claimsPrincipalMapper.ToSimpleEntity(authorizationCodeSource.Subject);
            var client = this.clientMapper.ToSimpleEntity(authorizationCodeSource.Client);
            var requestedScopes = this.scopeMapper.ToSimpleEntity(authorizationCodeSource.RequestedScopes);

            var rtn = base.ToSimpleEntity(source);

            rtn.Subject = subject;
            rtn.Client = client;
            rtn.Nonce = authorizationCodeSource.Nonce;
            rtn.RedirectUri = authorizationCodeSource.RedirectUri;
            rtn.CodeChallenge = authorizationCodeSource.CodeChallenge;
            rtn.CodeChallengeMethod = authorizationCodeSource.CodeChallengeMethod;
            rtn.CreationTime = authorizationCodeSource.CreationTime;
            rtn.IsOpenId = authorizationCodeSource.IsOpenId;
            rtn.RequestedScopes = requestedScopes;
            rtn.SessionId = authorizationCodeSource.SessionId;
            rtn.WasConsentShown = authorizationCodeSource.WasConsentShown;

            return rtn;
        }
    }
}