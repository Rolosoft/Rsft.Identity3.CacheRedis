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

// <copyright file="SimpleClaim.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Entities.Serialization
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The Simple Claim
    /// </summary>
    public sealed class SimpleClaim : SimpleBase
    {
        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        [JsonProperty("i")]
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the original issuer.
        /// </summary>
        [JsonProperty("o")]
        public string OriginalIssuer { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        [JsonProperty("p")]
        public IDictionary<string, string> Properties { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty("t")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("v")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the value.
        /// </summary>
        [JsonProperty("vt")]
        public string ValueType { get; set; }
    }
}