// <copyright file="RedisCacheConfigurationDefault.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Logic
{
    using Entities;
    using Interfaces;

    /// <summary>
    /// The default configuration.
    /// </summary>
    internal sealed class RedisCacheConfigurationDefault : IConfiguration<RedisCacheConfigurationEntity>
    {
        public RedisCacheConfigurationEntity Get => new RedisCacheConfigurationEntity();
    }
}