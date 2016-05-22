// <copyright file="ExceptionLoggingEventSource.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Diagnostics.EventSources
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.Tracing;
    using System.Globalization;

    using Common;

    /// <summary>
    /// The exception logging event source.
    /// </summary>
    public sealed partial class ExceptionLoggingEventSource
    {
        /// <summary>
        ///     The trace source
        /// </summary>
        private static readonly TraceSource TraceSource = new TraceSource(SourceNames.ExceptionLogging);

        /// <summary>
        /// The critical.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        [NonEvent]
        internal void Critical(Exception exception)
        {
            var formatExceptionLogEntry = FormatExceptionLogEntry(
                exception,
                TraceEventType.Critical,
                (int)EventIds.Critical);

            TraceSource.TraceEvent(TraceEventType.Critical, (int)EventIds.Critical, formatExceptionLogEntry);

            this.Critical(ModuleName, exception.Source, formatExceptionLogEntry);
        }

        /// <summary>
        /// The critical.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        [NonEvent]
        internal void Critical(string message)
        {
            TraceSource.TraceEvent(TraceEventType.Critical, (int)EventIds.Critical, message);

            this.Critical(ModuleName, string.Empty, message);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        [NonEvent]
        internal void Error(Exception exception)
        {
            var formatExceptionLogEntry = FormatExceptionLogEntry(exception, TraceEventType.Error, (int)EventIds.Error);

            TraceSource.TraceEvent(TraceEventType.Error, (int)EventIds.Error, formatExceptionLogEntry);

            this.Error(ModuleName, exception.Source, formatExceptionLogEntry);
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        [NonEvent]
        internal void Error(string message)
        {
            TraceSource.TraceEvent(TraceEventType.Error, (int)EventIds.Error, message);

            this.Error(ModuleName, string.Empty, message);
        }

        /// <summary>
        /// Informations the specified exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        [NonEvent]
        internal void Information(Exception exception)
        {
            var formatExceptionLogEntry = FormatExceptionLogEntry(
                exception,
                TraceEventType.Information,
                (int)EventIds.Informational);

            TraceSource.TraceEvent(TraceEventType.Information, (int)EventIds.Informational, formatExceptionLogEntry);

            this.Information(ModuleName, exception.Source, formatExceptionLogEntry);
        }

        /// <summary>
        /// The information.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        [NonEvent]
        internal void Information(string message)
        {
            TraceSource.TraceEvent(TraceEventType.Information, (int)EventIds.Informational, message);

            this.Information(ModuleName, string.Empty, message);
        }

        /// <summary>
        /// Verboses the specified exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        [NonEvent]
        internal void Verbose(Exception exception)
        {
            var formatExceptionLogEntry = FormatExceptionLogEntry(
                exception,
                TraceEventType.Verbose,
                (int)EventIds.Verbose);

            TraceSource.TraceEvent(TraceEventType.Verbose, (int)EventIds.Verbose, formatExceptionLogEntry);

            this.Verbose(ModuleName, exception.Source, formatExceptionLogEntry);
        }

        /// <summary>
        /// The verbose.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        [NonEvent]
        internal void Verbose(string message)
        {
            TraceSource.TraceEvent(TraceEventType.Verbose, (int)EventIds.Verbose, message);

            this.Verbose(ModuleName, string.Empty, message);
        }

        /// <summary>
        /// The warning.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        [NonEvent]
        internal void Warning(Exception exception)
        {
            var formatExceptionLogEntry = FormatExceptionLogEntry(
                exception,
                TraceEventType.Warning,
                (int)EventIds.Warning);

            TraceSource.TraceEvent(TraceEventType.Warning, (int)EventIds.Warning, formatExceptionLogEntry);

            this.Warning(ModuleName, exception.Source, formatExceptionLogEntry);
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        [NonEvent]
        internal void Warning(string message)
        {
            TraceSource.TraceEvent(TraceEventType.Warning, (int)EventIds.Warning, message);

            this.Warning(ModuleName, string.Empty, message);
        }

        /// <summary>
        /// Warnings the remote resource unavailable.
        /// </summary>
        /// <param name="remoteResource">
        /// The remote resource.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        [NonEvent]
        internal void WarningRemoteResourceUnavailable(string remoteResource, string message)
        {
            var formatLogEntry = string.Format(
                CultureInfo.InvariantCulture,
                Messages.WarningRemoteResourceUnavailable,
                ModuleName,
                string.Empty,
                remoteResource,
                message);

            TraceSource.TraceEvent(
                TraceEventType.Warning,
                (int)EventIds.WarningRemoteResourceUnavailable,
                formatLogEntry);

            this.WarningRemoteResourceUnavailable(ModuleName, string.Empty, remoteResource, message);
        }
    }
}