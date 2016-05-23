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