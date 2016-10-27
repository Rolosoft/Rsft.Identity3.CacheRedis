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

// <copyright file="SimpleClaimsIdentity.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Entities.Serialization
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The Simple Claims Identity
    /// </summary>
    public sealed class SimpleClaimsIdentity
    {
        /// <summary>
        /// Gets or sets the actor.
        /// </summary>
        [JsonProperty("a")]
        public SimpleClaimsIdentity Actor { get; set; }

        /// <summary>
        /// Gets or sets the type of the authentication.
        /// </summary>
        [JsonProperty("at")]
        public string AuthenticationType { get; set; }

        /// <summary>
        /// Gets or sets the bootstrap context.
        /// </summary>
        [JsonProperty("bc")]
        public object BootstrapContext { get; set; }

        /// <summary>
        /// Gets or sets the claims.
        /// </summary>
        [JsonProperty("c")]
        public IEnumerable<SimpleClaim> Claims { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [JsonProperty("l")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the type of the name claim.
        /// </summary>
        [JsonProperty("nct")]
        public string NameClaimType { get; set; }

        /// <summary>
        /// Gets or sets the type of the role claim.
        /// </summary>
        [JsonProperty("rct")]
        public string RoleClaimType { get; set; }
    }
}