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

// <copyright file="PropertyInfoStore.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Serialization
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using IdentityServer3.Core.Models;
    using Interfaces.Serialization;

    /// <summary>
    /// The Property Info Store
    /// </summary>
    /// <seealso cref="IPropertyInfoStore" />
    public sealed class PropertyInfoStore : IPropertyInfoStore
    {
        /// <summary>
        /// The cached property infos
        /// </summary>
        private static readonly ConcurrentDictionary<Type, Lazy<IEnumerable<PropertyInfo>>> CachedPropertyInfos =
            new ConcurrentDictionary<Type, Lazy<IEnumerable<PropertyInfo>>>();

        /// <summary>
        /// Gets the declared properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The <see cref="IEnumerable{T}" />
        /// </returns>
        public IEnumerable<PropertyInfo> GetDeclaredProperties(Type type)
        {
            return CachedPropertyInfos.GetOrAdd(type, ValueFactory(type)).Value;
        }

        /// <summary>
        /// Determines if the specified type is a standard type (i.e. not inherited).
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if [is base type] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsBaseType(Type type)
        {
            return type == typeof(AuthorizationCode) || type == typeof(Client) || type == typeof(ScopeClaim) ||
                   type == typeof(Scope) || type == typeof(Token) || type == typeof(RefreshToken);
        }

        /// <summary>
        /// Values the factory.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The <see cref="PropertyInfo" />
        /// </returns>
        private static Lazy<IEnumerable<PropertyInfo>> ValueFactory(Type type)
        {
            // If the type is not inherited from a base type, we do not need to get the custom properties (as there are none)
            if (IsBaseType(type))
            {
                return new Lazy<IEnumerable<PropertyInfo>>(() => new List<PropertyInfo>());
            }

            var rtn = new Lazy<IEnumerable<PropertyInfo>>(
                () => type.GetProperties(
                    BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public |
                    BindingFlags.SetProperty).Where(r => r.CanWrite));

            return rtn;
        }
    }
}