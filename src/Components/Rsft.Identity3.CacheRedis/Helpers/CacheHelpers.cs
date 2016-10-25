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

// <copyright file="CacheHelpers.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Helpers
{
    using System;

    /// <summary>
    /// The Cache Helpers
    /// </summary>
    public static class CacheHelpers
    {
        /// <summary>
        /// Gets the cache time span.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns>The <see cref="TimeSpan"/></returns>
        public static TimeSpan GetCacheTimeSpan(int seconds)
        {
            var cacheSeconds = seconds - 5;
            if (cacheSeconds < 5)
            {
                cacheSeconds = 5;
            }

            return TimeSpan.FromSeconds(cacheSeconds);
        }
    }
}