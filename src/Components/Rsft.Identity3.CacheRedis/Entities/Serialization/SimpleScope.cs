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

// <copyright file="ClaimLite.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Entities.Serialization
{
    using System.Collections.Generic;
    using IdentityServer3.Core.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// The Simple Scope
    /// </summary>
    internal sealed class SimpleScope
    {
        /// <summary>
        /// Gets or sets a value indicating whether [allow unrestricted introspection].
        /// </summary>
        [JsonProperty("aui")]
        public bool AllowUnrestrictedIntrospection { get; set; }

        /// <summary>
        /// Gets or sets the claims.
        /// </summary>
        [JsonProperty("c")]
        public List<ScopeClaim> Claims { get; set; }

        /// <summary>
        /// Gets or sets the claims rule.
        /// </summary>
        [JsonProperty("cr")]
        public string ClaimsRule { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("d")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [JsonProperty("dn")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SimpleScope"/> is emphasize.
        /// </summary>
        [JsonProperty("em")]
        public bool Emphasize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SimpleScope"/> is enabled.
        /// </summary>
        [JsonProperty("en")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include all claims for user].
        /// </summary>
        [JsonProperty("iacfu")]
        public bool IncludeAllClaimsForUser { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("n")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SimpleScope"/> is required.
        /// </summary>
        [JsonProperty("r")]
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the scope secrets.
        /// </summary>
        [JsonProperty("ss")]
        public List<Secret> ScopeSecrets { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show in discovery document].
        /// </summary>
        [JsonProperty("sidd")]
        public bool ShowInDiscoveryDocument { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty("t")]
        public ScopeType Type { get; set; }
    }
}