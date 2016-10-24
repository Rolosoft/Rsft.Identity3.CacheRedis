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
    using System.Diagnostics.Contracts;
    using System.Security.Claims;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces;

    /// <summary>
    /// Teh Authorization Code Mappers
    /// </summary>
    internal sealed class AuthorizationCodeMappers : BaseMapper<SimpleAuthorizationCode, AuthorizationCode>
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
        /// Initializes a new instance of the <see cref="AuthorizationCodeMappers"/> class.
        /// </summary>
        /// <param name="claimsPrincipalMapper">The claims principal mapper.</param>
        /// <param name="clientMapper">The client mapper.</param>
        /// <param name="scopeMapper">The scope mapper.</param>
        public AuthorizationCodeMappers(
            IMapper<SimpleClaimsPrincipal, ClaimsPrincipal> claimsPrincipalMapper,
            IMapper<SimpleClient, Client> clientMapper,
            IMapper<SimpleScope, Scope> scopeMapper)
        {
            Contract.Requires(claimsPrincipalMapper != null);
            Contract.Requires(clientMapper != null);
            Contract.Requires(scopeMapper != null);

            this.claimsPrincipalMapper = claimsPrincipalMapper;
            this.clientMapper = clientMapper;
            this.scopeMapper = scopeMapper;
        }

        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="AuthorizationCode"/></returns>
        public override AuthorizationCode ToComplexEntity(SimpleAuthorizationCode source)
        {
            if (source == null)
            {
                return null;
            }

            var subject = this.claimsPrincipalMapper.ToComplexEntity(source.Subject);
            var client = this.clientMapper.ToComplexEntity(source.Client);
            var requestedScopes = this.scopeMapper.ToComplexEntity(source.RequestedScopes);

            return new AuthorizationCode
            {
                Subject = subject,
                Client = client,
                Nonce = source.Nonce,
                RedirectUri = source.RedirectUri,
                CodeChallenge = source.CodeChallenge,
                CodeChallengeMethod = source.CodeChallengeMethod,
                CreationTime = source.CreationTime,
                IsOpenId = source.IsOpenId,
                RequestedScopes = requestedScopes,
                SessionId = source.SessionId,
                WasConsentShown = source.WasConsentShown
            };
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="SimpleAuthorizationCode"/></returns>
        public override SimpleAuthorizationCode ToSimpleEntity(AuthorizationCode source)
        {
            if (source == null)
            {
                return null;
            }

            var subject = this.claimsPrincipalMapper.ToSimpleEntity(source.Subject);
            var client = this.clientMapper.ToSimpleEntity(source.Client);
            var requestedScopes = this.scopeMapper.ToSimpleEntity(source.RequestedScopes);

            return new SimpleAuthorizationCode
            {
                Subject = subject,
                Client = client,
                Nonce = source.Nonce,
                RedirectUri = source.RedirectUri,
                CodeChallenge = source.CodeChallenge,
                CodeChallengeMethod = source.CodeChallengeMethod,
                CreationTime = source.CreationTime,
                IsOpenId = source.IsOpenId,
                RequestedScopes = requestedScopes,
                SessionId = source.SessionId,
                WasConsentShown = source.WasConsentShown
            };
        }
    }
}