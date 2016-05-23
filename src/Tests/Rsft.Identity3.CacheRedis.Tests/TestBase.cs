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