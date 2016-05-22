// <copyright file="EventIds.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Diagnostics.Common
{
    /// <summary>
    /// Event Identifiers
    /// </summary>
    internal enum EventIds
    {
        /// <summary>
        ///     The none.
        /// </summary>
        None = 0,

        /// <summary>
        ///     The initializing.
        /// </summary>
        Initializing = 1100,

        /// <summary>
        ///     The initialized
        /// </summary>
        Initialized = 1101,

        /// <summary>
        ///     The timer logging
        /// </summary>
        TimerLogging = 3100,

        /// <summary>
        ///     The critical
        /// </summary>
        Critical = 9901,

        /// <summary>
        ///     The warning
        /// </summary>
        Warning = 4501,

        /// <summary>
        ///     The warning remote resource unavailable
        /// </summary>
        WarningRemoteResourceUnavailable = 4502,

        /// <summary>
        ///     The verbose
        /// </summary>
        Verbose = 3502,

        /// <summary>
        ///     The error
        /// </summary>
        Error = 5901,

        /// <summary>
        ///     The informational
        /// </summary>
        Informational = 3501,

        /// <summary>
        ///     The log always
        /// </summary>
        LogAlways = 3503,

        /// <summary>
        ///     The method enter
        /// </summary>
        MethodEnter = 3101,

        /// <summary>
        ///     The method exit
        /// </summary>
        MethodExit = 3102,

        /// <summary>
        ///     The server response string
        /// </summary>
        ServerResponseString = 3105,

        /// <summary>
        /// The cache hit
        /// </summary>
        CacheHit = 3106,

        /// <summary>
        /// The cache miss
        /// </summary>
        CacheMiss = 3107,

        /// <summary>
        /// The cache get object
        /// </summary>
        CacheGetObject = 3108,

        /// <summary>
        /// The cache set object
        /// </summary>
        CacheSetObject = 3109
    }
}