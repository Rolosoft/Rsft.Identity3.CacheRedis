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

// <copyright file="RedisCacheFactory.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading;
    using Caches;
    using Entities;
    using Helpers;
    using IdentityServer3.Core.Models;
    using IdentityServer3.Core.Services;
    using Interfaces;
    using Logic;
    using Microsoft.Azure;
    using StackExchange.Redis;
    using Stores;

    /// <summary>
    /// The Redis cache factory.
    /// </summary>
    public static class RedisCacheFactory
    {
        /// <summary>
        /// The cloud configuration redis connection string key
        /// </summary>
        private const string CloudConfigurationRedisConnectionStringKey = @"Rsft:Identity3:CacheConnectionString";

        /// <summary>
        /// The lazy caches
        /// </summary>
        private static readonly Lazy<Entities.Caches> LazyCaches = new Lazy<Entities.Caches>(Initialize, LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// The connection multiplexer lazy
        /// </summary>
        private static Lazy<ConnectionMultiplexer> connectionMultiplexerLazy;

        /// <summary>
        /// The incoming configuration
        /// </summary>
        private static IConfiguration<RedisCacheConfigurationEntity> incomingConfiguration;

        /// <summary>
        /// The incoming connection multiplexer
        /// </summary>
        private static ConnectionMultiplexer incomingConnectionMultiplexer;

        /// <summary>
        /// The incoming redis connection string
        /// </summary>
        private static string incomingRedisConnectionString;

        /// <summary>
        /// Gets the connection multiplexer.
        /// </summary>
        /// <value>
        /// The connection multiplexer.
        /// </value>
        private static ConnectionMultiplexer ConnectionMultiplexer
        {
            get
            {
                if (incomingConnectionMultiplexer != null)
                {
                    return incomingConnectionMultiplexer;
                }

                if (connectionMultiplexerLazy == null)
                {
                    try
                    {
                        connectionMultiplexerLazy = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(RedisConnectionString));
                    }
                    catch (Exception exception)
                    {
                        exception.Log();
                    }
                }

                ConnectionMultiplexer connectionMultiplexer = null;

                try
                {
                    connectionMultiplexer = connectionMultiplexerLazy?.Value;
                }
                catch (Exception exception)
                {
                    exception.Log();
                }

                return connectionMultiplexer;
            }
        }

        /// <summary>
        /// Gets the redis connection string.
        /// </summary>
        /// <value>
        /// The redis connection string.
        /// </value>
        private static string RedisConnectionString
        {
            get
            {
                /*Try incoming connection string*/
                if (!string.IsNullOrWhiteSpace(incomingRedisConnectionString))
                {
                    return incomingRedisConnectionString;
                }

                /*Try existing connection string on MUX*/
                if (incomingConnectionMultiplexer != null)
                {
                    return incomingConnectionMultiplexer.Configuration;
                }

                /*Get from .config file*/
                var settingFromConfig = CloudConfigurationManager.GetSetting(CloudConfigurationRedisConnectionStringKey);

                var s = settingFromConfig ?? @"--unknown--";

                return s;
            }
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        private static IConfiguration<RedisCacheConfigurationEntity> Configuration => incomingConfiguration ?? new RedisCacheConfigurationDefault();

        /// <summary>
        /// Creates the specified connection multiplexer.
        /// </summary>
        /// <param name="redisConnectionString">The redis connection string.</param>
        /// <param name="connectionMultiplexer">The connection multiplexer.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        /// <see cref="Tuple" /> containing singleton instances of objects to be used with Identity Server.
        /// <para>Item 1: Client store cache (<see cref="ICache{T}" />)</para><para>Item 2: Scope store cache (<see cref="ICache{T}" />)</para><para>Item 3: User service cache (<see cref="ICache{T}" />)</para>
        /// </returns>
        public static Entities.Caches Create(
            string redisConnectionString = null,
            ConnectionMultiplexer connectionMultiplexer = null,
            IConfiguration<RedisCacheConfigurationEntity> configuration = null)
        {
            if (incomingRedisConnectionString == null)
            {
                incomingRedisConnectionString = redisConnectionString;
            }

            if (incomingConnectionMultiplexer == null)
            {
                incomingConnectionMultiplexer = connectionMultiplexer;
            }

            if (incomingConfiguration == null)
            {
                incomingConfiguration = configuration;
            }

            return LazyCaches.Value;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>The <see cref="Caches"/></returns>
        private static Entities.Caches Initialize()
        {
            var jsonSettingsFactory = new JsonSettingsFactory(new ClientMapperBase<Client>());

            var authorizationCacheManager = new RedisCacheManager<AuthorizationCode>(
                ConnectionMultiplexer,
                Configuration,
                jsonSettingsFactory);

            var authorizationCodeStore = new AuthorizationCodeStore(
                authorizationCacheManager,
                Configuration);

            var clientCacheManager = new RedisCacheManager<Client>(
                ConnectionMultiplexer,
                Configuration,
                jsonSettingsFactory);

            var clientCache = new ClientStoreCache(
                clientCacheManager,
                Configuration);

            var refreshTokenCacheManager = new RedisCacheManager<RefreshToken>(
                ConnectionMultiplexer,
                Configuration,
                jsonSettingsFactory);

            var refreshTokenStore = new RefreshTokenStore(
                refreshTokenCacheManager,
                Configuration);

            var scopeCacheManager = new RedisCacheManager<IEnumerable<Scope>>(
                ConnectionMultiplexer,
                Configuration,
                jsonSettingsFactory);

            var scopesCache = new ScopeStoreCache(
                scopeCacheManager,
                Configuration);

            var redisHandleCacheManager = new RedisCacheManager<Token>(
                ConnectionMultiplexer,
                Configuration,
                jsonSettingsFactory);

            var tokenHandleStore = new TokenHandleStore(
                redisHandleCacheManager,
                Configuration);

            var userServiceCacheManager = new RedisCacheManager<IEnumerable<Claim>>(
                ConnectionMultiplexer,
                Configuration,
                jsonSettingsFactory);

            var userServiceCache = new UserServiceCache(
                userServiceCacheManager,
                Configuration);

            var caches = new Entities.Caches
            {
                AuthorizationCodeStore = authorizationCodeStore,
                ClientCache = clientCache,
                RefreshTokenStore = refreshTokenStore,
                ScopesCache = scopesCache,
                TokenHandleStore = tokenHandleStore,
                UserServiceCache = userServiceCache
            };

            return caches;
        }
    }
}