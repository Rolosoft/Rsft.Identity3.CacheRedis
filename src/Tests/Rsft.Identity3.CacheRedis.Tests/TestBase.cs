// <copyright file="TestBase.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Tests
{
    using System;
    using System.Diagnostics.Tracing;
    using Diagnostics.EventSources;
    using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;

    public abstract class TestBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TestBase" /> class.
        /// </summary>
        protected TestBase()
        {
            var observableEventListener = new ObservableEventListener();

            observableEventListener.EnableEvents(ExceptionLoggingEventSource.Log, EventLevel.LogAlways);

            observableEventListener.LogToConsole();
        }

        /// <summary>
        /// The write time elapsed.
        /// </summary>
        /// <param name="timerElapsed">
        /// The timer elapsed.
        /// </param>
        protected static void WriteTimeElapsed(long timerElapsed)
        {
            Console.WriteLine("Elapsed timer: {0}ms", timerElapsed);
        }

        /// <summary>
        /// The write time elapsed.
        /// </summary>
        /// <param name="timerElapsed">
        /// The timer elapsed.
        /// </param>
        protected static void WriteTimeElapsed(TimeSpan timerElapsed)
        {
            Console.WriteLine("Elapsed timer: {0}", timerElapsed);
        }
    }
}