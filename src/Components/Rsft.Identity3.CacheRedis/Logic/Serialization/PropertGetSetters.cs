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

// <copyright file="PropertGetSetters.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Serialization
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Interfaces.Serialization;

    /// <summary>
    /// The Propert Get Setters
    /// </summary>
    public sealed class PropertGetSetters : IPropertGetSetters
    {
        /// <summary>
        /// The getters
        /// </summary>
        private static readonly ConcurrentDictionary<Type, Dictionary<string, Func<object, object>>> Getters =
            new ConcurrentDictionary<Type, Dictionary<string, Func<object, object>>>();

        /// <summary>
        /// The setters
        /// </summary>
        private static readonly ConcurrentDictionary<Type, Dictionary<string, Action<object, object>>> Setters =
            new ConcurrentDictionary<Type, Dictionary<string, Action<object, object>>>();

        /// <summary>
        /// Gets the getters.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="IEnumerable{T}" />
        /// </returns>
        public Dictionary<string, Func<object, object>> GetGetters(Type t)
        {
            return Getters.GetOrAdd(t, GetValueFactory(t));
        }

        /// <summary>
        /// Gets the setters.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="IEnumerable{T}" />
        /// </returns>
        public Dictionary<string, Action<object, object>> GetSetters(Type t)
        {
            return Setters.GetOrAdd(t, SetValueFactory(t));
        }

        /// <summary>
        /// Gets the getter.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// The <see cref="" />
        /// </returns>
        private static Func<object, object> GetGetter(PropertyInfo propertyInfo)
        {
            var instance = Expression.Parameter(propertyInfo.DeclaringType, "i");

            var property = Expression.Property(instance, propertyInfo);

            var convert = Expression.TypeAs(property, typeof(object));

            return Expression.Lambda<Func<object, object>>(convert, instance).Compile();
        }

        /// <summary>
        /// Gets the setter.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The <see cref="Action{T,T}"/></returns>
        private static Action<object, object> GetSetter(Type targetType, string propertyName)
        {
            var target = Expression.Parameter(typeof(object), "obj");
            var value = Expression.Parameter(typeof(object), "value");
            var property = targetType.GetProperty(propertyName);
            var body = Expression.Assign(
                Expression.Property(Expression.Convert(target, property.DeclaringType), property),
                Expression.Convert(value, property.PropertyType));

            var lambda = Expression.Lambda<Action<object, object>>(body, target, value);
            return lambda.Compile();
        }

        /// <summary>
        /// Gets the value factory.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        private static Dictionary<string, Func<object, object>> GetValueFactory(Type t)
        {
            var props = t.GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            var dictionary = props.ToDictionary(r => r.Name, r => GetGetter(r));
            return dictionary;
        }

        /// <summary>
        /// Sets the value factory.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        private static Dictionary<string, Action<object, object>> SetValueFactory(Type t)
        {
            var props = t.GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            var dictionary = props.ToDictionary(r => r.Name, r => GetSetter(t, r.Name));
            return dictionary;
        }
    }
}