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

// <copyright file="RedisCacheManager.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

// ReSharper disable StaticMemberInGenericType
namespace Rsft.Identity3.CacheRedis.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Diagnostics.EventSources;
    using Entities;
    using Helpers;
    using Interfaces;
    using Newtonsoft.Json;
    using StackExchange.Redis;
    
    /// <summary>
    /// The redis cache manager.
    /// </summary>
    /// <typeparam name="T">Type of object to cache.</typeparam>
    internal sealed class RedisCacheManager<T> : ICacheManager<T>
    {
        /// <summary>
        /// The logging source name base
        /// </summary>
        private const string LoggingSourceNameBase = @"Rsft.Identity3.CacheRedis.Logic.RedisCacheManager";

        /// <summary>
        /// The cache configuration
        /// </summary>
        private readonly IConfiguration<RedisCacheConfigurationEntity> cacheConfiguration;

        /// <summary>
        /// The connection multiplexer
        /// </summary>
        private readonly ConnectionMultiplexer connectionMultiplexer;

        /// <summary>
        /// The json serializer settings
        /// </summary>
        private readonly JsonSerializerSettings jsonSerializerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheManager{T}" /> class.
        /// </summary>
        /// <param name="connectionMultiplexer">The connection multiplexer.</param>
        /// <param name="cacheConfiguration">The cache configuration.</param>
        /// <param name="jsonSerializerSettings">The json serializer settings.</param>
        public RedisCacheManager(
            ConnectionMultiplexer connectionMultiplexer,
            IConfiguration<RedisCacheConfigurationEntity> cacheConfiguration,
            JsonSerializerSettings jsonSerializerSettings)
        {
            Contract.Requires(connectionMultiplexer != null);
            Contract.Requires(cacheConfiguration != null);
            Contract.Requires(jsonSerializerSettings != null);

            this.connectionMultiplexer = connectionMultiplexer;
            this.cacheConfiguration = cacheConfiguration;
            this.jsonSerializerSettings = jsonSerializerSettings;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string key)
        {
            var fqMethodLogName = LoggingNaming.GetFqMethodLogName(LoggingSourceNameBase, "SetAsync");

            ActivityLoggingEventSource.Log.MethodEnter(fqMethodLogName);

            var database = this.connectionMultiplexer.GetDatabase();
            var s = this.GetKey(key);

            try
            {
                var stopWatch = Stopwatch.StartNew();
                await database.KeyDeleteAsync(s).ConfigureAwait(false);
                stopWatch.Stop();
                ActivityLoggingEventSource.Log.TimerLogging(fqMethodLogName, stopWatch.ElapsedMilliseconds);
            }
            catch (AggregateException aggregateException)
            {
                aggregateException.Handle(
                    ae =>
                    {
                        ae.Log();
                        return true;
                    });
            }
            catch (Exception exception)
            {
                exception.Log();
            }

            ActivityLoggingEventSource.Log.MethodExit(fqMethodLogName);
        }

        /// <inheritdoc />
        public async Task<IDictionary<string, T>> GetAllAsync(IEnumerable<string> keys)
        {
            var fqMethodLogName = LoggingNaming.GetFqMethodLogName(LoggingSourceNameBase, "GetAllAsync");

            ActivityLoggingEventSource.Log.MethodEnter(fqMethodLogName);

            var database = this.connectionMultiplexer.GetDatabase();

            var redisKeys = keys.Select(r => (RedisKey)r).ToArray();

            RedisValue[] redisValues = null;

            try
            {
                redisValues = await database.StringGetAsync(redisKeys).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                exception.Log();
            }

            var rtn = new Dictionary<string, T>();

            if (redisValues != null
                && redisValues.AnySafe())
            {
                for (var index = 0; index < redisValues.Length; index++)
                {
                    var redisKey = redisKeys[index];
                    var redisValue = redisValues[index];

                    var deserializedObject = default(T);

                    if (redisValue != RedisValue.Null
                        && redisValue.HasValue)
                    {
                        var s1 = redisValue.ToString();

                        var s = this.cacheConfiguration.Get.UseObjectCompression ? s1.Decompress() : s1;

                        deserializedObject = JsonConvert.DeserializeObject<T>(s, this.jsonSerializerSettings);
                    }

                    rtn.Add(redisKey, deserializedObject);
                }

                ActivityLoggingEventSource.Log.CacheHit(fqMethodLogName);
                ActivityLoggingEventSource.Log.MethodExit(fqMethodLogName);

                return rtn;
            }

            ActivityLoggingEventSource.Log.CacheMiss(fqMethodLogName);
            ActivityLoggingEventSource.Log.MethodExit(fqMethodLogName);

            return rtn;
        }

        /// <inheritdoc />
        public async Task<T> GetAsync(string key)
        {
            var fqMethodLogName = LoggingNaming.GetFqMethodLogName(LoggingSourceNameBase, "GetAsync");

            ActivityLoggingEventSource.Log.MethodEnter(fqMethodLogName);

            var database = this.connectionMultiplexer.GetDatabase();
            var s = this.GetKey(key);

            var redisValue = default(RedisValue);

            try
            {
                var stopwatch = Stopwatch.StartNew();
                redisValue = await database.StringGetAsync(s).ConfigureAwait(false);
                stopwatch.Stop();
                ActivityLoggingEventSource.Log.TimerLogging(fqMethodLogName, stopwatch.ElapsedMilliseconds);
            }
            catch (AggregateException aggregateException)
            {
                aggregateException.Handle(
                    ae =>
                    {
                        ae.Log();
                        return true;
                    });
            }
            catch (Exception exception)
            {
                exception.Log();
            }

            if (redisValue == RedisValue.Null
                || redisValue == default(RedisValue)
                || redisValue.IsNullOrEmpty
                || !redisValue.HasValue)
            {
                ActivityLoggingEventSource.Log.CacheMiss(fqMethodLogName);
                ActivityLoggingEventSource.Log.MethodExit(fqMethodLogName);
                return default(T);
            }

            ActivityLoggingEventSource.Log.CacheHit(fqMethodLogName);

            var s1 = redisValue.ToString();
            var decompressOrNo = this.cacheConfiguration.Get.UseObjectCompression ? s1.Decompress() : s1;

            ActivityLoggingEventSource.Log.CacheGetObject(fqMethodLogName, decompressOrNo);

            var x = typeof(T);

            var deserializedObject = JsonConvert.DeserializeObject<T>(decompressOrNo, this.jsonSerializerSettings);

            ActivityLoggingEventSource.Log.MethodExit(fqMethodLogName);

            return deserializedObject;
        }

        /// <inheritdoc />
        public async Task SetAsync(string key, T item, TimeSpan timeSpan)
        {
            var fqMethodLogName = LoggingNaming.GetFqMethodLogName(LoggingSourceNameBase, "SetAsync");

            ActivityLoggingEventSource.Log.MethodEnter(fqMethodLogName);

            var database = this.connectionMultiplexer.GetDatabase();
            var s = this.GetKey(key);

            var serializeObject = JsonConvert.SerializeObject(item, this.jsonSerializerSettings);

            ActivityLoggingEventSource.Log.CacheSetObject(fqMethodLogName, serializeObject);

            var compressOrNo = this.cacheConfiguration.Get.UseObjectCompression ? serializeObject.Compress() : serializeObject;

            try
            {
                var stopWatch = Stopwatch.StartNew();
                await database.StringSetAsync(s, compressOrNo, timeSpan).ConfigureAwait(false);
                stopWatch.Stop();
                ActivityLoggingEventSource.Log.TimerLogging(fqMethodLogName, stopWatch.ElapsedMilliseconds);
            }
            catch (AggregateException aggregateException)
            {
                aggregateException.Handle(
                    ae =>
                    {
                        ae.Log();
                        return true;
                    });
            }
            catch (Exception exception)
            {
                exception.Log();
            }

            ActivityLoggingEventSource.Log.MethodExit(fqMethodLogName);
        }

        /// <summary>
        /// Gets the cache key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The <see cref="string"/> defining the cache key.</returns>
        private string GetKey(string key)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(key));

            return string.Format(CultureInfo.InvariantCulture, @"{0}_{1}", this.cacheConfiguration.Get.RedisCacheDefaultPrefix, key);
        }
    }
}