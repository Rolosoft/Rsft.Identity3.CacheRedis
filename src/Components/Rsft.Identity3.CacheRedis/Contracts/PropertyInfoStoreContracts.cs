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

// <copyright file="PropertyInfoStoreContracts.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Reflection;
    using Interfaces.Serialization;

    /// <summary>
    /// The Property Info Store Contracts
    /// </summary>
    /// <seealso cref="IPropertyInfoStore" />
    [ContractClassFor(typeof(IPropertyInfoStore))]
    internal abstract class PropertyInfoStoreContracts : IPropertyInfoStore
    {
        /// <summary>
        /// Gets the declared properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The <see cref="IEnumerable{T}" />
        /// </returns>
        public IEnumerable<PropertyInfo> GetDeclaredProperties(Type type)
        {
            Contract.Requires(type != null);
            Contract.Ensures(Contract.Result<IEnumerable<PropertyInfo>>() != null);

            return default(IEnumerable<PropertyInfo>);
        }
    }
}