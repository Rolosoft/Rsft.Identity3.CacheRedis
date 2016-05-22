// <copyright file="LoggingNaming.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Helpers
{
    internal static class LoggingNaming
    {
        /// <summary>
        /// Gets the name of the FQ method log.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>Fully qualified method name.</returns>
        public static string GetFqMethodLogName(string className, string methodName)
        {
            return string.Concat(className, ".", methodName);
        }
    }
}