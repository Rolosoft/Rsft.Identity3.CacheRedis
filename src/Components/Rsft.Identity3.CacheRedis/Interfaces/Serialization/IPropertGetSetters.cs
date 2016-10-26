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

// <copyright file="IPropertGetSetters.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Interfaces.Serialization
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Property Get Setters
    /// </summary>
    public interface IPropertGetSetters
    {
        /// <summary>
        /// Gets the getters.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        Dictionary<string, Func<object, object>> GetGetters(Type t);

        /// <summary>
        /// Gets the setters.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        Dictionary<string, Action<object, object>> GetSetters(Type t);
    }
}