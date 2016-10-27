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

// <copyright file="PropertyGetSettersTyped.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Entities;
    using Interfaces.Serialization;

    /// <summary>
    /// The PropertGetSettersTyped
    /// </summary>
    /// <typeparam name="TType">The type of the type.</typeparam>
    /// <seealso cref="IPropertyGetSettersTyped{TType}" />
    internal sealed class PropertyGetSettersTyped<TType> : IPropertyGetSettersTyped<TType>
    {
        /// <summary>
        /// The getters
        /// </summary>
        private static Dictionary<string, Func<TType, object>> getters;

        /// <summary>
        /// The setters
        /// </summary>
        private static Dictionary<string, TypedSetter<TType>> setters;

        /// <summary>
        /// The property information store
        /// </summary>
        private readonly IPropertyInfoStore propertyInfoStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyGetSettersTyped{TType}"/> class.
        /// </summary>
        /// <param name="propertyInfoStore">The property information store.</param>
        public PropertyGetSettersTyped(IPropertyInfoStore propertyInfoStore)
        {
            Contract.Requires(propertyInfoStore != null);

            this.propertyInfoStore = propertyInfoStore;
        }

        /// <summary>
        /// Gets the getters.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="T:System.Collections.Generic.IEnumerable`1" />
        /// </returns>
        public Dictionary<string, Func<TType, object>> GetGetters(Type t)
        {
            if (getters == null)
            {
                getters = this.GetValueFactory(t);
            }

            return getters;
        }

        /// <summary>
        /// Gets the setters.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="T:System.Collections.Generic.IEnumerable`1" />
        /// </returns>
        public Dictionary<string, TypedSetter<TType>> GetSetters(Type t)
        {
            if (setters == null)
            {
                setters = this.SetValueFactory(t);
            }

            return setters;
        }

        /// <summary>
        /// Gets the getter.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>
        /// The <see cref="" />
        /// </returns>
        private static Func<TType, object> GetGetter(PropertyInfo propertyInfo)
        {
            var instance = Expression.Parameter(propertyInfo.DeclaringType, "i");
            var property = Expression.Property(instance, propertyInfo);
            var convert = Expression.TypeAs(property, typeof(object));

            return Expression.Lambda<Func<TType, object>>(convert, instance).Compile();
        }

        /// <summary>
        /// Gets the setter.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The <see cref="Action{T,T}"/></returns>
        private static Action<TType, object> GetSetter(Type targetType, string propertyName)
        {
            var target = Expression.Parameter(typeof(object), "obj");
            var value = Expression.Parameter(typeof(object), "value");
            var property = targetType.GetProperty(propertyName);
            var body = Expression.Assign(
                Expression.Property(Expression.Convert(target, property.DeclaringType), property),
                Expression.Convert(value, property.PropertyType));

            var lambda = Expression.Lambda<Action<TType, object>>(body, target, value);
            return lambda.Compile();
        }

        /// <summary>
        /// Gets the value factory.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="Dictionary{TKey,TValue}" />
        /// </returns>
        private Dictionary<string, Func<TType, object>> GetValueFactory(Type t)
        {
            var props = this.propertyInfoStore.GetDeclaredProperties(t);
            var dictionary = props.ToDictionary(r => r.Name, GetGetter);
            return dictionary;
        }

        /// <summary>
        /// Sets the value factory.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Dictionary{TKey,TValue}"/></returns>
        private Dictionary<string, TypedSetter<TType>> SetValueFactory(Type t)
        {
            var props = this.propertyInfoStore.GetDeclaredProperties(t);
            var dictionary = props.ToDictionary(r => r.Name, r => new TypedSetter<TType>
            {
                OriginalType = r.PropertyType,
                Setter = GetSetter(t, r.Name)
            });
            return dictionary;
        }
    }
}