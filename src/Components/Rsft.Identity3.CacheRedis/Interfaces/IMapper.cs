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

// <copyright file="IMapper.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// The Mapper Interface
    /// </summary>
    /// <typeparam name="TSimpleEntity">The type of the simple entity.</typeparam>
    /// <typeparam name="TComplexEntity">The type of the complex entity.</typeparam>
    internal interface IMapper<TSimpleEntity, TComplexEntity>
        where TSimpleEntity : class
        where TComplexEntity : class
    {
        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The TComplexEntity</returns>
        TComplexEntity ToComplexEntity(TSimpleEntity source);

        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        IEnumerable<TComplexEntity> ToComplexEntity(IEnumerable<TSimpleEntity> source);

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The TSimpleEntity</returns>
        TSimpleEntity ToSimpleEntity(TComplexEntity source);

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        IEnumerable<TSimpleEntity> ToSimpleEntity(IEnumerable<TComplexEntity> source);
    }
}