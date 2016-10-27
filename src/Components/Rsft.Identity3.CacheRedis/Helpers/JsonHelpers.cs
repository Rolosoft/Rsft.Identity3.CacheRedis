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

// <copyright file="JsonHelpers.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Helpers
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// The JSON Helpers
    /// </summary>
    public static class JsonHelpers
    {
        /// <summary>
        /// Maps the json number.
        /// </summary>
        /// <param name="originalType">Type of the original.</param>
        /// <param name="value">The value.</param>
        /// <returns>The Corrected Type</returns>
        public static object MapJsonNumber(Type originalType, object value)
        {
            if (originalType == typeof(int) && value is long)
            {
                return (int)(long)value;
            }

            return value;
        }

        /// <summary>
        /// Safes the deserialize.
        /// </summary>
        /// <typeparam name="T">The Type</typeparam>
        /// <param name="serializer">The serializer.</param>
        /// <param name="reader">The reader.</param>
        /// <returns>
        /// The T
        /// </returns>
        public static T SafeDeserialize<T>(this JsonSerializer serializer, JsonReader reader)
        {
            if (reader == null)
            {
                return default(T);
            }

            return serializer.Deserialize<T>(reader);
        }
    }
}