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

// <copyright file="GenericMapper.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Mappers
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using Entities.Serialization;
    using Helpers;
    using Interfaces.Serialization;

    /// <summary>
    /// The Generic Mapper
    /// </summary>
    /// <typeparam name="TSimpleType">The type of the simple type.</typeparam>
    /// <typeparam name="TComplexType">The type of the complex type.</typeparam>
    /// <seealso cref="Rsft.Identity3.CacheRedis.Interfaces.IMapper{TSimpleType, TComplexType}" />
    public class GenericMapper<TSimpleType, TComplexType> : BaseMapper<TSimpleType, TComplexType>
        where TSimpleType : SimpleBase, new()
        where TComplexType : class, new()
    {
        /// <summary>
        /// The property mapper
        /// </summary>
        private readonly IPropertyGetSettersTyped<TComplexType> propertyMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericMapper{TSimpleType, TComplexType}"/> class.
        /// </summary>
        /// <param name="propertyMapper">The property mapper.</param>
        internal GenericMapper(IPropertyGetSettersTyped<TComplexType> propertyMapper)
        {
            Contract.Requires(propertyMapper != null);

            this.propertyMapper = propertyMapper;
        }

        /// <summary>
        /// To the complex entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The TComplexEntity
        /// </returns>
        public override TComplexType ToComplexEntity(TSimpleType source)
        {
            var setters = this.propertyMapper.GetSetters(typeof(TComplexType));

            var complex = new TComplexType();

            foreach (var setter in setters)
            {
                var value = source.DataBag[setter.Key];

                // if it's an int, this should handle the mapping for it
                var correctedValue = JsonHelpers.MapJsonNumber(setter.Value.OriginalType, value);

                setter.Value.Setter(complex, correctedValue);
            }

            return complex;
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The TSimpleEntity
        /// </returns>
        public override TSimpleType ToSimpleEntity(object source)
        {
            var getters = this.propertyMapper.GetGetters(typeof(TComplexType));

            var simple = new TSimpleType { DataBag = new Dictionary<string, object>() };

            foreach (var getter in getters)
            {
                simple.DataBag.Add(getter.Key, getter.Value((TComplexType)source));
            }

            return simple;
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The <see cref="T:System.Collections.Generic.IEnumerable`1" />
        /// </returns>
        public IEnumerable<TSimpleType> ToSimpleEntity(IEnumerable<TComplexType> source)
        {
            return source?.Select(this.ToSimpleEntity) ?? new List<TSimpleType>();
        }
    }
}