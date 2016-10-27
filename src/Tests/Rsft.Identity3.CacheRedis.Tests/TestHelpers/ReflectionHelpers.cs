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

// <copyright file="ReflectionHelpers.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
namespace Rsft.Identity3.CacheRedis.Tests.TestHelpers
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// The Reflection Helpers
    /// </summary>
    public static class ReflectionHelpers
    {
        /// <summary>
        /// Gets the getter.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>The <see cref="Func{TResult, TType}"/></returns>
        public static Func<TType, object> GetGetter<TType>(this PropertyInfo propertyInfo)
        {
            var instance = Expression.Parameter(propertyInfo.DeclaringType, "i");
            var property = Expression.Property(instance, propertyInfo);
            var convert = Expression.TypeAs(property, typeof(object));

            return Expression.Lambda<Func<TType, object>>(convert, instance).Compile();
        }

        /// <summary>
        /// Gets the setter.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The <see cref="Action{T,T}"/></returns>
        public static Action<TType, object> GetSetter<TType>(this Type targetType, string propertyName)
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
    }
}