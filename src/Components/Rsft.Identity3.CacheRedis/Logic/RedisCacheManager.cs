// <copyright file="RedisCacheManager.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

// ReSharper disable StaticMemberInGenericType
namespace Rsft.Identity3.CacheRedis.Logic
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using StackExchange.Redis.Extensions.Core;
    using StackExchange.Redis.Extensions.Newtonsoft;

    /// <summary>
    /// The redis cache manager.
    /// </summary>
    /// <typeparam name="T">Type of object to cache.</typeparam>
    internal sealed class RedisCacheManager<T> : ICacheManager<T>
    {
        /// <summary>
        /// The json serializer settings lazy
        /// </summary>
        private static readonly Lazy<JsonSerializerSettings> JsonSerializerSettingsLazy = new Lazy<JsonSerializerSettings>(() => new JsonSerializerSettings());

        /// <summary>
        /// The claim converter lazy
        /// </summary>
        private static readonly Lazy<ClaimConverter> ClaimConverterLazy = new Lazy<ClaimConverter>(() => new ClaimConverter());

        /// <summary>
        /// The newtonsoft serializer lazy
        /// </summary>
        private static readonly Lazy<NewtonsoftSerializer> NewtonsoftSerializerLazy = new Lazy<NewtonsoftSerializer>(() => new NewtonsoftSerializer(JsonSerializerSettingsLazy.Value));

        /// <summary>
        /// The stack exchange redis cache client lazy
        /// </summary>
        private static Lazy<StackExchangeRedisCacheClient> stackExchangeRedisCacheClientLazy;

        /// <summary>
        /// The connection multiplexer
        /// </summary>
        private readonly ConnectionMultiplexer connectionMultiplexer;

        /// <summary>
        /// The cache configuration
        /// </summary>
        private readonly IConfiguration<RedisCacheConfigurationEntity> cacheConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheManager{T}" /> class.
        /// </summary>
        /// <param name="connectionMultiplexer">The connection multiplexer.</param>
        /// <param name="cacheConfiguration">The cache configuration.</param>
        public RedisCacheManager(
            ConnectionMultiplexer connectionMultiplexer,
            IConfiguration<RedisCacheConfigurationEntity> cacheConfiguration)
        {
            Contract.Requires(connectionMultiplexer != null);
            Contract.Requires(cacheConfiguration != null);

            this.connectionMultiplexer = connectionMultiplexer;
            this.cacheConfiguration = cacheConfiguration;

            this.Initialize();
        }

        /// <summary>
        /// Gets the serializer settings.
        /// </summary>
        /// <value>
        /// The serializer settings.
        /// </value>
        private static JsonSerializerSettings SerializerSettings => JsonSerializerSettingsLazy.Value;

        /// <summary>
        /// Gets the newtonsoft serializer.
        /// </summary>
        /// <value>
        /// The newtonsoft serializer.
        /// </value>
        private static NewtonsoftSerializer NewtonsoftSerializer => NewtonsoftSerializerLazy.Value;

        /// <summary>
        /// Gets the stack exchange redis cache client.
        /// </summary>
        /// <value>
        /// The stack exchange redis cache client.
        /// </value>
        private static StackExchangeRedisCacheClient StackExchangeRedisCacheClient
                    => stackExchangeRedisCacheClientLazy.Value;

        /// <summary>
        /// Gets the claim converter.
        /// </summary>
        /// <value>
        /// The claim converter.
        /// </value>
        private static ClaimConverter ClaimConverter => ClaimConverterLazy.Value;

        /// <summary>
        /// Gets the cache item asynchronously.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<T> GetAsync(string key, CancellationToken cancellationToken)
        {
            var s = this.GetKey(key);

            var foo = await StackExchangeRedisCacheClient.GetAsync<T>(s).ConfigureAwait(false);

            return foo;
        }

        /// <summary>
        /// Sets the asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="expiresAtOffset">The expires at offset.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task SetAsync(string key, T item, DateTimeOffset expiresAtOffset, CancellationToken cancellationToken)
        {
            var s = this.GetKey(key);

            await StackExchangeRedisCacheClient.AddAsync(s, item, expiresAtOffset).ConfigureAwait(false);
        }

        private string GetKey(string key)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(key));

            return string.Format(CultureInfo.InvariantCulture, @"{0}_{1}", this.cacheConfiguration.Get.RedisCacheDefaultPrefix, key);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            SerializerSettings.Converters.Add(ClaimConverter);

            if (stackExchangeRedisCacheClientLazy == null)
            {
                stackExchangeRedisCacheClientLazy = new Lazy<StackExchangeRedisCacheClient>(() => new StackExchangeRedisCacheClient(this.connectionMultiplexer, NewtonsoftSerializer));
            }
        }
    }
}