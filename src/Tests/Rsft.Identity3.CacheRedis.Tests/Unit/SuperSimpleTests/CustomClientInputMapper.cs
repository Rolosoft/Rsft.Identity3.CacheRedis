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

// <copyright file="CustomClientInputMapper.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Unit.SuperSimpleTests
{
    using System.Security.Claims;
    using CacheRedis.Logic.Mappers;
    using Entities.Serialization;
    using IdentityServer3.Core.Models;
    using Interfaces;

    public sealed class CustomClientInputMapper : DefaultClientInputMapper<Client>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomClientInputMapper"/> class.
        /// </summary>
        /// <param name="claimsMapper">The claims mapper.</param>
        public CustomClientInputMapper(IInputMapper<SimpleClaim, Claim> claimsMapper)
            : base(claimsMapper)
        {
        }
    }
}