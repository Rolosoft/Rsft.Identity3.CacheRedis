// <copyright file="IConfiguration.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>

namespace Rsft.Identity3.CacheRedis.Interfaces
{
    public interface IConfiguration<out T>
    {
         T Get { get; }
    }
}