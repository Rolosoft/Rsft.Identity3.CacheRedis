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

// <copyright file="CustomClientConverter.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Serialization
{
    using System;
    using System.Diagnostics.Contracts;
    using Entities.Serialization;
    using Helpers;
    using IdentityServer3.Core.Models;
    using Interfaces;
    using Newtonsoft.Json;

    /// <summary>
    /// The Generic Converter
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    internal sealed class CustomClientConverter : JsonConverter
    {
        /// <summary>
        /// The input mapper
        /// </summary>
        private readonly IInputMapper<SimpleClient, Client> inputMapper;

        /// <summary>
        /// The output mapper
        /// </summary>
        private readonly IOutputMapper<SimpleClient, Client> outputMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomClientConverter"/> class.
        /// </summary>
        /// <param name="inputMapper">The input mapper.</param>
        /// <param name="outputMapper">The output mapper.</param>
        public CustomClientConverter(
            IInputMapper<SimpleClient, Client> inputMapper,
            IOutputMapper<SimpleClient, Client> outputMapper)
        {
            Contract.Requires(inputMapper != null);
            Contract.Requires(outputMapper != null);

            this.inputMapper = inputMapper;
            this.outputMapper = outputMapper;
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType != null && (objectType == typeof(Client) || objectType.IsSubclassOf(typeof(Client)));
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var source = serializer.SafeDeserialize<SimpleClient>(reader);

            var rtn = this.outputMapper.Map(source);

            return rtn;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var source = (Client)value;

            var target = this.inputMapper.Map(source);

            serializer.Serialize(writer, target);
        }
    }
}