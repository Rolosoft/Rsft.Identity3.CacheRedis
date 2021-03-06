﻿// Copyright 2016 Rolosoft Ltd
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// <copyright file="GenericConverter.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Serialization
{
    using System;
    using System.Diagnostics.Contracts;
    using Helpers;
    using Interfaces;
    using Newtonsoft.Json;

    /// <summary>
    /// The Generic Converter
    /// </summary>
    /// <typeparam name="TSimpleEntity">The type of the simple entity.</typeparam>
    /// <typeparam name="TComplexEntity">The type of the complex entity.</typeparam>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    internal sealed class GenericConverter<TSimpleEntity, TComplexEntity> : JsonConverter
        where TSimpleEntity : class
        where TComplexEntity : class
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper<TSimpleEntity, TComplexEntity> mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericConverter{TSimpleEntity, TComplexEntity}"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        public GenericConverter(IMapper<TSimpleEntity, TComplexEntity> mapper)
        {
            Contract.Requires(mapper != null);

            this.mapper = mapper;
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
            return objectType != null && (objectType == typeof(TComplexEntity) || objectType.IsSubclassOf(typeof(TComplexEntity)));
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
            var source = serializer.SafeDeserialize<TSimpleEntity>(reader);
            var target = this.mapper.ToComplexEntity(source);

            return target;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var source = (TComplexEntity)value;
            var target = this.mapper.ToSimpleEntity(source);

            serializer.Serialize(writer, target);
        }
    }
}