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

// <copyright file="SimpleToken.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Entities.Serialization
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The Simple Token
    /// </summary>
    internal sealed class SimpleToken
    {
        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        [JsonProperty("a")]
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the claims.
        /// </summary>
        [JsonProperty("c")]
        public List<SimpleClaim> Claims { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        [JsonProperty("cl")]
        public SimpleClient Client { get; set; }

        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        [JsonProperty("ct")]
        public DateTimeOffset CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        [JsonProperty("i")]
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the lifetime.
        /// </summary>
        [JsonProperty("l")]
        public int Lifetime { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty("t")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [JsonProperty("v")]
        public int Version { get; set; }
    }
}