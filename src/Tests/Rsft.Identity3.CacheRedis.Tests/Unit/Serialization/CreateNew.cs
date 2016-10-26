// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateNew.cs" company="Email Hippo Ltd">
//   © Email Hippo Ltd
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Rsft.Identity3.CacheRedis.Tests.Unit.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using IdentityServer3.Core.Models;
    using TestHelpers;

    internal static class CreateNew
    {
        /// <summary>
        /// The props
        /// </summary>
        private static IEnumerable<PropertyInfo> props = null;

        /// <summary>
        /// The setter
        /// </summary>
        private static Action<object> setter = null;

        /// <summary>
        /// The action
        /// </summary>
        private static Action<object, object> theAction = null;

        /// <summary>
        /// The delegate
        /// </summary>
        private static Delegate theDelegate = null;

        /// <summary>
        /// The function
        /// </summary>
        private static Func<object> theFunc = null;

        /// <summary>
        /// Creates the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="simple">The simple.</param>
        /// <returns>
        /// The Object
        /// </returns>
        public static object Create(Type t, CustomSimple simple)
        {
            var buildLambda = BuildLambda(t);
            var instance = (Client)buildLambda();

            var action = GetSetter(instance, "AppId");

            instance.ClientId = simple.ClientId;

            foreach (var prop in GetProperties(t))
            {
                action(instance, simple.DataBag[prop.Name]);
            }

            return instance;
        }

        /// <summary>
        /// Builds the lambda.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        private static Func<object> BuildLambda(Type t)
        {
            if (theFunc != null)
            {
                return theFunc;
            }

            var createdType = t;
            var ctor = Expression.New(createdType);
            var memberInit = Expression.MemberInit(ctor);

            theFunc = Expression.Lambda<Func<object>>(memberInit).Compile();

            return theFunc;
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        private static IEnumerable<PropertyInfo> GetProperties(Type t)
        {
            if (props != null)
            {
                return props;
            }

            props = t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(r => r.Name == "AppId");

            return props;
        }

        /// <summary>
        /// Gets the setter.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        private static Action<object, object> GetSetter(Expression<Func<object, object>> property)
        {
            if (theAction != null)
            {
                return theAction;
            }

            var memberExp = (MemberExpression)property.Body;
            var propInfo = (PropertyInfo)memberExp.Member;
            MethodInfo setter = propInfo.GetSetMethod();
            Delegate del = Delegate.CreateDelegate(typeof(Action<object, object>), setter);
            theAction = (Action<object, object>)del;

            return theAction;
        }

        private static Action<object, object> GetSetter<T>(T obj, string propertyName)
        {
            if (theAction != null)
            {
                return theAction;
            }

            ParameterExpression targetExpr = Expression.Parameter(obj.GetType(), "Target");
            MemberExpression propExpr = Expression.Property(targetExpr, propertyName);
            ParameterExpression valueExpr = Expression.Parameter(typeof(object), "value");
            MethodCallExpression convertExpr = Expression.Call(typeof(Convert), "ChangeType", null, valueExpr, Expression.Constant(propExpr.Type));
            UnaryExpression valueCast = Expression.Convert(convertExpr, propExpr.Type);
            BinaryExpression assignExpr = Expression.Assign(propExpr, valueCast);
            theAction = Expression.Lambda<Action<object, object>>(assignExpr, targetExpr, valueExpr).Compile();

            return theAction;
        }
    }
}