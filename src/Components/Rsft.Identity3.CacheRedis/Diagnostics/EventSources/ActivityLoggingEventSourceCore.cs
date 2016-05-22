// <copyright file="ActivityLoggingEventSource.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Diagnostics.EventSources
{
    using System;
    using System.Diagnostics.Tracing;

    using Common;

    /// <summary>
    ///     The activity logging event source.
    /// </summary>
    [EventSource(Name = SourceNames.ActivityLogging)]
    public partial class ActivityLoggingEventSource : EventSource
    {
        /// <summary>
        ///     The module name
        /// </summary>
        private const string ModuleName = @"ActivityLogging";

        /// <summary>
        ///     The instance.
        /// </summary>
        private static readonly Lazy<ActivityLoggingEventSource> Instance =
            new Lazy<ActivityLoggingEventSource>(() => new ActivityLoggingEventSource());

        /// <summary>
        ///     Prevents a default instance of the <see cref="ActivityLoggingEventSource" /> class from being created.
        /// </summary>
        private ActivityLoggingEventSource()
        {
        }

        /// <summary>
        ///     Gets the log.
        /// </summary>
        public static ActivityLoggingEventSource Log => Instance.Value;

        /// <summary>
        /// Initializeds the specified module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="source">The source.</param>
        [Event(
            (int)EventIds.Initialized,
            Message = Messages.Initialized,
            Level = EventLevel.Verbose,
            Keywords = Keywords.GeneralDiagnostic)]
        private void Initialized(string module, string source)
        {
            if (this.IsEnabled(EventLevel.Verbose, Keywords.GeneralDiagnostic))
            {
                this.WriteEvent((int)EventIds.Initialized, module, source);
            }
        }

        /// <summary>
        /// The initializing.
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        [Event(
            (int)EventIds.Initializing,
            Message = Messages.Initializing,
            Level = EventLevel.Verbose,
            Keywords = Keywords.GeneralDiagnostic)]
        private void Initializing(string module, string source)
        {
            if (this.IsEnabled(EventLevel.Verbose, Keywords.GeneralDiagnostic))
            {
                this.WriteEvent((int)EventIds.Initializing, module, source);
            }
        }

        /// <summary>
        /// The method enter.
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        [Event(
            (int)EventIds.MethodEnter,
            Message = Messages.MethodEnter,
            Level = EventLevel.Verbose,
            Keywords = Keywords.GeneralDiagnostic)]
        private void MethodEnter(string module, string source)
        {
            if (this.IsEnabled(EventLevel.Verbose, Keywords.GeneralDiagnostic))
            {
                this.WriteEvent((int)EventIds.MethodEnter, module, source);
            }
        }

        /// <summary>
        /// The method exit.
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        [Event(
            (int)EventIds.MethodExit,
            Message = Messages.MethodExit,
            Level = EventLevel.Verbose,
            Keywords = Keywords.GeneralDiagnostic)]
        private void MethodExit(string module, string source)
        {
            if (this.IsEnabled(EventLevel.Verbose, Keywords.GeneralDiagnostic))
            {
                this.WriteEvent((int)EventIds.MethodExit, module, source);
            }
        }

        /// <summary>
        /// The timer logging.
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="elapsed">
        /// The elapsed milliseconds.
        /// </param>
        [Event(
            (int)EventIds.TimerLogging,
            Message = Messages.TimerLogging,
            Level = EventLevel.Verbose,
            Keywords = Keywords.Perf)]
        private void TimerLogging(string module, string source, long elapsed)
        {
            if (this.IsEnabled(EventLevel.Verbose, Keywords.Perf))
            {
                this.WriteEvent((int)EventIds.TimerLogging, module, source, elapsed);
            }
        }

        /// <summary>
        /// The server response string.
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="serverResponse">
        /// The server response.
        /// </param>
        [Event(
            (int)EventIds.ServerResponseString,
            Message = Messages.ServerResponseString,
            Level = EventLevel.Verbose,
            Keywords = Keywords.Io)]
        private void ServerResponseString(string module, string source, string serverResponse)
        {
            if (this.IsEnabled(EventLevel.Verbose, Keywords.Io))
            {
                this.WriteEvent((int)EventIds.ServerResponseString, module, source, serverResponse);
            }
        }

        /// <summary>
        /// Caches the hit.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="source">The source.</param>
        [Event(
            (int)EventIds.CacheHit,
            Message = Messages.CacheHit,
            Level = EventLevel.Informational,
            Keywords = Keywords.Cache)]
        private void CacheHit(string module, string source)
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.Cache))
            {
                this.WriteEvent((int)EventIds.CacheHit, module, source);
            }
        }

        /// <summary>
        /// Caches the miss.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="source">The source.</param>
        [Event(
            (int)EventIds.CacheMiss,
            Message = Messages.CacheMiss,
            Level = EventLevel.Informational,
            Keywords = Keywords.Cache)]
        private void CacheMiss(string module, string source)
        {
            if (this.IsEnabled(EventLevel.Informational, Keywords.Cache))
            {
                this.WriteEvent((int)EventIds.CacheMiss, module, source);
            }
        }

        /// <summary>
        /// Caches the get object.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="source">The source.</param>
        /// <param name="myObject">My object.</param>
        [Event(
            (int)EventIds.CacheGetObject,
            Message = Messages.CacheGetObject,
            Level = EventLevel.Verbose,
            Keywords = Keywords.Cache)]
        private void CacheGetObject(string module, string source, string myObject)
        {
            if (this.IsEnabled(EventLevel.Verbose, Keywords.Cache))
            {
                this.WriteEvent((int)EventIds.CacheGetObject, module, source, myObject);
            }
        }

        /// <summary>
        /// Caches the set object.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="source">The source.</param>
        /// <param name="myObject">My object.</param>
        [Event(
            (int)EventIds.CacheSetObject,
            Message = Messages.CacheSetObject,
            Level = EventLevel.Verbose,
            Keywords = Keywords.Cache)]
        private void CacheSetObject(string module, string source, string myObject)
        {
            if (this.IsEnabled(EventLevel.Verbose, Keywords.Cache))
            {
                this.WriteEvent((int)EventIds.CacheSetObject, module, source, myObject);
            }
        }

        /// <summary>
        ///     The keywords.
        /// </summary>
        public sealed class Keywords
        {
            /// <summary>
            ///     The general diagnostic.
            /// </summary>
            public const EventKeywords GeneralDiagnostic = (EventKeywords)1;

            /// <summary>
            ///     The IO keyword.
            /// </summary>
            public const EventKeywords Io = (EventKeywords)2;

            /// <summary>
            ///     The performance keyword.
            /// </summary>
            public const EventKeywords Perf = (EventKeywords)4;

            /// <summary>
            /// The cache keyword
            /// </summary>
            public const EventKeywords Cache = (EventKeywords)8;
        }
    }
}