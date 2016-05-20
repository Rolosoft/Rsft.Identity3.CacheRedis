// <copyright file="RedisCacheFactory.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using Entities;
    using IdentityServer3.Core.Models;
    using IdentityServer3.Core.Services;
    using Interfaces;
    using Logic;

    using Microsoft.Azure;

    using StackExchange.Redis;

    public static class RedisCacheFactory
    {
        private const string CloudConfigurationRedisConnectionStringKey = @"Identity3.Redis";

        private static Lazy<ConnectionMultiplexer> connectionMultiplexerLazy;

        private static IConfiguration<RedisCacheConfigurationEntity> incomingConfiguration;

        private static string incomingRedisConnectionString;

        private static Lazy<IConfiguration<RedisCacheConfigurationEntity>> configurationLazy;

        private static Lazy<ICache<Client>> cacheClientLazy;

        private static Lazy<ICache<IEnumerable<Scope>>> cacheScopeLazy;

        private static Lazy<ICache<IEnumerable<Claim>>> cacheUserServiceLazy;

        private static ConnectionMultiplexer incomingConnectionMultiplexer;

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
        public static Tuple<ICache<Client>, ICache<IEnumerable<Scope>>, ICache<IEnumerable<Claim>>> Create(
            string redisConnectionString = null,
            ConnectionMultiplexer connectionMultiplexer = null,
            IConfiguration<RedisCacheConfigurationEntity> configuration = null)
        {
            incomingRedisConnectionString = redisConnectionString;
            incomingConnectionMultiplexer = connectionMultiplexer;
            incomingConfiguration = configuration;

            Initialize();

            return new Tuple<ICache<Client>, ICache<IEnumerable<Scope>>, ICache<IEnumerable<Claim>>>(CacheClient, CacheScope, CacheUsers);

        }

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
                    connectionMultiplexerLazy = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(RedisConnectionString));
                }

                return connectionMultiplexerLazy.Value;
            }
        }

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
                return CloudConfigurationManager.GetSetting(CloudConfigurationRedisConnectionStringKey);
            }
        }

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

        private static ICache<Client> CacheClient => cacheClientLazy.Value;

        private static ICache<IEnumerable<Scope>> CacheScope => cacheScopeLazy.Value;

        private static ICache<IEnumerable<Claim>> CacheUsers => cacheUserServiceLazy.Value;

        private static void Initialize()
        {
            
        }

    }
}