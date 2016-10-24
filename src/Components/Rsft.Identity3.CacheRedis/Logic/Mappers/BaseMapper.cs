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

// <copyright file="BaseMapper.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Mappers
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    /// <summary>
    /// The Base Mapper
    /// </summary>
    /// <typeparam name="TSimpleEntity">The type of the simple entity.</typeparam>
    /// <typeparam name="TComplexEntity">The type of the complex entity.</typeparam>
    /// <seealso cref="Interfaces.IMapper{TSimpleEntity, TComplexEntity}" />
    public abstract class BaseMapper<TSimpleEntity, TComplexEntity> : IMapper<TSimpleEntity, TComplexEntity>
        where TComplexEntity : class
        where TSimpleEntity : class
    {
        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The TComplexEntity
        /// </returns>
        public abstract TComplexEntity ToComplexEntity(TSimpleEntity source);

        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The <see cref="T:System.Collections.Generic.IEnumerable`1" />
        /// </returns>
        public IEnumerable<TComplexEntity> ToComplexEntity(IEnumerable<TSimpleEntity> source)
        {
            return source?.Select(this.ToComplexEntity) ?? new List<TComplexEntity>();
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The TSimpleEntity
        /// </returns>
        public abstract TSimpleEntity ToSimpleEntity(TComplexEntity source);

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The <see cref="T:System.Collections.Generic.IEnumerable`1" />
        /// </returns>
        public IEnumerable<TSimpleEntity> ToSimpleEntity(IEnumerable<TComplexEntity> source)
        {
            return source?.Select(this.ToSimpleEntity) ?? new List<TSimpleEntity>();
        }
    }
}