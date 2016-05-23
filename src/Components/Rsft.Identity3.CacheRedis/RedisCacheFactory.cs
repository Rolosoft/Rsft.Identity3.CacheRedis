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
    using Caches;
    using Entities;
    using Helpers;
    using IdentityServer3.Core.Models;
    using IdentityServer3.Core.Services;
    using Interfaces;
    using Logic;
    using Microsoft.Azure;
    using StackExchange.Redis;

    public static class RedisCacheFactory
    {
        /// <summary>
        /// The cloud configuration redis connection string key
        /// </summary>
        private const string CloudConfigurationRedisConnectionStringKey = @"Rsft:Identity3:CacheConnectionString";

        /// <summary>
        /// The caches lazy
        /// </summary>
        private static readonly Lazy<Entities.Caches> CachesLazy = new Lazy<Entities.Caches>(() => new Entities.Caches());

        /// <summary>
        /// The connection multiplexer lazy
        /// </summary>
        private static Lazy<ConnectionMultiplexer> connectionMultiplexerLazy;

        /// <summary>
        /// The incoming configuration
        /// </summary>
        private static IConfiguration<RedisCacheConfigurationEntity> incomingConfiguration;

        /// <summary>
        /// The incoming redis connection string
        /// </summary>
        private static string incomingRedisConnectionString;

        /// <summary>
        /// The configuration lazy
        /// </summary>
        private static Lazy<IConfiguration<RedisCacheConfigurationEntity>> configurationLazy;

        /// <summary>
        /// The cache client lazy
        /// </summary>
        private static Lazy<ICache<Client>> cacheClientLazy;

        /// <summary>
        /// The cache scope lazy
        /// </summary>
        private static Lazy<ICache<IEnumerable<Scope>>> cacheScopeLazy;

        /// <summary>
        /// The cache user service lazy
        /// </summary>
        private static Lazy<ICache<IEnumerable<Claim>>> cacheUserServiceLazy;

        /// <summary>
        /// The client cache manager lazy
        /// </summary>
        private static Lazy<ICacheManager<Client>> clientCacheManagerLazy;

        /// <summary>
        /// The scopes cache manager lazy
        /// </summary>
        private static Lazy<ICacheManager<IEnumerable<Scope>>> scopesCacheManagerLazy;

        /// <summary>
        /// The user service manager lazy
        /// </summary>
        private static Lazy<ICacheManager<IEnumerable<Claim>>> userServiceManagerLazy;

        /// <summary>
        /// The incoming connection multiplexer
        /// </summary>
        private static ConnectionMultiplexer incomingConnectionMultiplexer;

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
                        connectionMultiplexerLazy =
                            new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(RedisConnectionString));
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
        private static IConfiguration<RedisCacheConfigurationEntity> Configuration
        {
            get
            {
                /*Try incoming configuration*/
                if (incomingConfiguration != null)
                {
                    return incomingConfiguration;
                }

                /*Use default config*/
                if (configurationLazy == null)
                {
                    configurationLazy = new Lazy<IConfiguration<RedisCacheConfigurationEntity>>(() => new RedisCacheConfigurationDefault());
                }

                return configurationLazy.Value;
            }
        }

        /// <summary>
        /// Gets the cache manager client.
        /// </summary>
        /// <value>
        /// The cache manager client.
        /// </value>
        private static ICacheManager<Client> CacheManagerClient => clientCacheManagerLazy.Value;

        /// <summary>
        /// Gets the cache manager scopes.
        /// </summary>
        /// <value>
        /// The cache manager scopes.
        /// </value>
        private static ICacheManager<IEnumerable<Scope>> CacheManagerScopes => scopesCacheManagerLazy.Value;

        /// <summary>
        /// Gets the cache user service.
        /// </summary>
        /// <value>
        /// The cache user service.
        /// </value>
        private static ICacheManager<IEnumerable<Claim>> CacheUserService => userServiceManagerLazy.Value;

        /// <summary>
        /// Gets the caches.
        /// </summary>
        /// <value>
        /// The caches.
        /// </value>
        private static Entities.Caches Caches => CachesLazy.Value;

        /// <summary>
        /// Gets the cache client.
        /// </summary>
        /// <value>
        /// The cache client.
        /// </value>
        private static ICache<Client> CacheClient => cacheClientLazy.Value;

        /// <summary>
        /// Gets the cache scope.
        /// </summary>
        /// <value>
        /// The cache scope.
        /// </value>
        private static ICache<IEnumerable<Scope>> CacheScope => cacheScopeLazy.Value;

        /// <summary>
        /// Gets the cache users.
        /// </summary>
        /// <value>
        /// The cache users.
        /// </value>
        private static ICache<IEnumerable<Claim>> CacheUsers => cacheUserServiceLazy.Value;

        /// <summary>
        /// Creates the specified connection multiplexer.
        /// </summary>
        /// <param name="redisConnectionString">The redis connection string.</param>
        /// <param name="connectionMultiplexer">The connection multiplexer.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        /// <see cref="Tuple" /> containing singleton instances of objects to be used with Identity Server.
        /// <para>Item 1: Client store cache (<see cref="ICache{Client}" />)</para><para>Item 2: Scope store cache (<see cref="ICache{IEnumerable{Scope}}" />)</para><para>Item 3: User service cache (<see cref="ICache{IEnumerable{Claim}}" />)</para>
        /// </returns>
        public static Entities.Caches Create(
            string redisConnectionString = null,
            ConnectionMultiplexer connectionMultiplexer = null,
            IConfiguration<RedisCacheConfigurationEntity> configuration = null)
        {
            incomingRedisConnectionString = redisConnectionString;
            incomingConnectionMultiplexer = connectionMultiplexer;
            incomingConfiguration = configuration;

            Initialize();

            Caches.ClientCache = CacheClient;
            Caches.ScopesCache = CacheScope;
            Caches.UserServiceCache = CacheUsers;

            return Caches;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private static void Initialize()
        {
            if (clientCacheManagerLazy == null)
            {
                clientCacheManagerLazy = new Lazy<ICacheManager<Client>>(() => new RedisCacheManager<Client>(ConnectionMultiplexer, Configuration));
            }

            if (scopesCacheManagerLazy == null)
            {
                scopesCacheManagerLazy = new Lazy<ICacheManager<IEnumerable<Scope>>>(() => new RedisCacheManager<IEnumerable<Scope>>(ConnectionMultiplexer, Configuration));
            }

            if (userServiceManagerLazy == null)
            {
                userServiceManagerLazy = new Lazy<ICacheManager<IEnumerable<Claim>>>(() => new RedisCacheManager<IEnumerable<Claim>>(ConnectionMultiplexer, Configuration));
            }

            if (cacheClientLazy == null)
            {
                cacheClientLazy = new Lazy<ICache<Client>>(() => new ClientStoreCache(CacheManagerClient, Configuration));
            }

            if (cacheScopeLazy == null)
            {
                cacheScopeLazy = new Lazy<ICache<IEnumerable<Scope>>>(() => new ScopeStoreCache(CacheManagerScopes, Configuration));
            }

            if (cacheUserServiceLazy == null)
            {
                cacheUserServiceLazy = new Lazy<ICache<IEnumerable<Claim>>>(() => new UserServiceCache(CacheUserService, Configuration));
            }
        }
    }
}