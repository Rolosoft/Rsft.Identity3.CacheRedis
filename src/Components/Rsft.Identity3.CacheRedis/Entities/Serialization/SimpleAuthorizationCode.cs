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

// <copyright file="SimpleAuthorizationCode.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Entities.Serialization
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The Simple Authorization Code
    /// </summary>
    internal sealed class SimpleAuthorizationCode
    {
        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        [JsonProperty("c")]
        public SimpleClient Client { get; set; }

        /// <summary>
        /// Gets or sets the code challenge.
        /// </summary>
        [JsonProperty("cc")]
        public string CodeChallenge { get; set; }

        /// <summary>
        /// Gets or sets the code challenge method.
        /// </summary>
        [JsonProperty("ccm")]
        public string CodeChallengeMethod { get; set; }

        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        [JsonProperty("ct")]
        public DateTimeOffset CreationTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is open identifier.
        /// </summary>
        [JsonProperty("i")]
        public bool IsOpenId { get; set; }

        /// <summary>
        /// Gets or sets the nonce.
        /// </summary>
        [JsonProperty("n")]
        public string Nonce { get; set; }

        /// <summary>
        /// Gets or sets the redirect URI.
        /// </summary>
        [JsonProperty("ru")]
        public string RedirectUri { get; set; }

        /// <summary>
        /// Gets or sets the requested scopes.
        /// </summary>
        [JsonProperty("rs")]
        public IEnumerable<SimpleScope> RequestedScopes { get; set; }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        [JsonProperty("si")]
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        [JsonProperty("s")]
        public SimpleClaimsPrincipal Subject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [was consent shown].
        /// </summary>
        [JsonProperty("wcs")]
        public bool WasConsentShown { get; set; }
    }
}