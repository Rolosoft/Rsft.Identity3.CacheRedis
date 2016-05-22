// <copyright file="ExceptionLogging.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Helpers
{
    using System;

    internal static class ExceptionLogging
    {
        public static void Log(this Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            Diagnostics.EventSources.ExceptionLoggingEventSource.Log.Error(exception);
        }
    }
}