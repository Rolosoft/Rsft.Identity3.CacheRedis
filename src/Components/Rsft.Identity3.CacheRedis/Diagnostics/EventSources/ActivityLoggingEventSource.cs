// <copyright file="ActivityLoggingEventSource.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Diagnostics.EventSources
{
    using System.Diagnostics;
    using System.Diagnostics.Tracing;

    using Common;

    public partial class ActivityLoggingEventSource
    {
        /// <summary>
        ///     The trace source
        /// </summary>
        private static readonly TraceSource TraceSource = new TraceSource(SourceNames.ActivityLogging);

        /// <summary>
        /// Initializeds the specified module.
        /// </summary>
        /// <param name="source">The source.</param>
        [NonEvent]
        public void Initialized(string source)
        {
            TraceSource.TraceEvent(TraceEventType.Start, (int)EventIds.Initialized, Messages.Initialized, source);

            this.Initialized(ModuleName, source);
        }

        /// <summary>
        /// The initializing.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        [NonEvent]
        public void Initializing(string source)
        {
            TraceSource.TraceEvent(TraceEventType.Start, (int)EventIds.Initializing, Messages.Initializing, source);

            this.Initializing(ModuleName, source);
        }

        /// <summary>
        /// The method enter.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        [NonEvent]
        public void MethodEnter(string source)
        {
            TraceSource.TraceEvent(TraceEventType.Verbose, (int)EventIds.MethodEnter, Messages.MethodEnter, source);

            this.MethodEnter(ModuleName, source);
        }

        /// <summary>
        /// The method exit.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        [NonEvent]
        public void MethodExit(string source)
        {
            TraceSource.TraceEvent(TraceEventType.Verbose, (int)EventIds.MethodExit, Messages.MethodExit, source);

            this.MethodExit(ModuleName, source);
        }

        /// <summary>
        /// The timer logging.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="elapsed">
        /// The elapsed milliseconds.
        /// </param>
        [NonEvent]
        public void TimerLogging(string source, long elapsed)
        {
            TraceSource.TraceEvent(TraceEventType.Verbose, (int)EventIds.TimerLogging, Messages.TimerLogging, source, elapsed);

            this.TimerLogging(ModuleName, source, elapsed);
        }

        /// <summary>
        /// The server response string.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="serverResponse">
        /// The server response.
        /// </param>
        [NonEvent]
        public void ServerResponseString(string source, string serverResponse)
        {
            TraceSource.TraceEvent(TraceEventType.Verbose, (int)EventIds.ServerResponseString, Messages.ServerResponseString, source);

            this.ServerResponseString(ModuleName, source, serverResponse);
        }

        [NonEvent]
        public void CacheHit(string source)
        {
            TraceSource.TraceEvent(TraceEventType.Information, (int)EventIds.CacheHit, Messages.CacheHit, source);

            this.CacheHit(ModuleName, source);
        }

        /// <summary>
        /// Caches the miss.
        /// </summary>
        /// <param name="source">The source.</param>
        [NonEvent]
        public void CacheMiss(string source)
        {
            TraceSource.TraceEvent(TraceEventType.Information, (int)EventIds.CacheMiss, Messages.CacheMiss, source);

            this.CacheMiss(ModuleName, source);
        }

        /// <summary>
        /// Caches the get object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="myObject">My object.</param>
        [NonEvent]
        public void CacheGetObject(string source, string myObject)
        {
            TraceSource.TraceEvent(TraceEventType.Verbose, (int)EventIds.CacheGetObject, Messages.CacheGetObject, source, myObject);

            this.CacheGetObject(ModuleName, source, myObject);
        }

        /// <summary>
        /// Caches the set object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="myObject">My object.</param>
        [NonEvent]
        public void CacheSetObject(string source, string myObject)
        {
            TraceSource.TraceEvent(TraceEventType.Verbose, (int)EventIds.CacheSetObject, Messages.CacheSetObject, source, myObject);

            this.CacheSetObject(ModuleName, source, myObject);
        }
    }
}