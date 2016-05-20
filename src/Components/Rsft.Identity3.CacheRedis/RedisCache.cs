// <copyright file="RedisCache.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis
{
    using System;
    using System.Threading.Tasks;
    using IdentityServer3.Core.Services;

    internal sealed class RedisCache<T> : ICache<T>
        where T : class
    {
        public Task<T> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(string key, T item)
        {
            throw new NotImplementedException();
        }
    }
}