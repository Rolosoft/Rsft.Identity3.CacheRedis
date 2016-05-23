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

// <copyright file="Messages.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Diagnostics.Common
{
    /// <summary>
    /// The messages.
    /// </summary>
    internal static class Messages
    {
        /// <summary>
        ///     The critical
        /// </summary>
        public const string Critical = @"Critical. Module:{0}, Source:{1}, Error:{2}";

        /// <summary>
        ///     The error
        /// </summary>
        public const string Error = @"Error. Module:{0}, Source:{1}, Error:{2}";

        /// <summary>
        ///     The informational
        /// </summary>
        public const string Informational = @"Informational. Module:{0}, Source:{1}, Message:{2}";

        /// <summary>
        ///     The initialized.
        /// </summary>
        public const string Initialized = @"Initialization complete. Module:{0}, Source:{1}";

        /// <summary>
        ///     The initializing.
        /// </summary>
        public const string Initializing = @"Initializing. Module:{0}, Source:{1}";

        /// <summary>
        ///     The log always
        /// </summary>
        public const string LogAlways = @"LogAlways: Module:{0}, Source:{1}, Message:{2}";

        /// <summary>
        ///     The method enter
        /// </summary>
        public const string MethodEnter = @"MethodEnter. Module:{0}, Source:{1}";

        /// <summary>
        ///     The method exit
        /// </summary>
        public const string MethodExit = @"MethodExit. Module:{0}, Source:{1}";

        /// <summary>
        ///     The timer logging
        /// </summary>
        public const string TimerLogging = @"Timer logging. Module:{0}, Source:{1}, Elapsed:{2}ms";

        /// <summary>
        ///     The verbose
        /// </summary>
        public const string Verbose = @"Verbose. Module:{0}, Source:{1}, Message:{2}";

        /// <summary>
        ///     The warning.
        /// </summary>
        public const string Warning = @"Warning. Module:{0}, Source:{1}, Message:{2}";

        /// <summary>
        ///     The warning remote resource unavailable
        /// </summary>
        public const string WarningRemoteResourceUnavailable =
            @"Warning remote source is unavailable. Module:{0}, Source:{1}, RemoteResource:{2}, Message{3}";

        /// <summary>
        ///     The server response string
        /// </summary>
        public const string ServerResponseString = @"ServerResponseString. Module:{0}, Source:{1}, ServerResponse:{2}";

        /// <summary>
        /// The cache hit
        /// </summary>
        public const string CacheHit = @"Cache hit. Module:{0}, Source:{1}";

        /// <summary>
        /// The cache miss
        /// </summary>
        public const string CacheMiss = @"Cache miss. Module:{0}, Source:{1}";

        /// <summary>
        /// The cache get object
        /// </summary>
        public const string CacheGetObject = @"CacheGetObject. Module:{0}, Source:{1}, Object:{2}";

        /// <summary>
        /// The cache insert object
        /// </summary>
        public const string CacheSetObject = @"CacheInsertObject. Module:{0}, Source:{1}, Object:{2}";
    }
}