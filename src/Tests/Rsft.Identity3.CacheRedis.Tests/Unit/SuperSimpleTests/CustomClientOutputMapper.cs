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

// <copyright file="CustomClientOutputMapper.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Unit.SuperSimpleTests
{
    using System.Security.Claims;
    using CacheRedis.Logic.Mappers;
    using Entities.Serialization;
    using Interfaces;
    using TestHelpers;

    public sealed class CustomClientOutputMapper : DefaultClientOutputMapper<CustomClient>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomClientOutputMapper"/> class.
        /// </summary>
        /// <param name="claimsMapper">The claims mapper.</param>
        public CustomClientOutputMapper(IOutputMapper<SimpleClaim, Claim> claimsMapper)
            : base(claimsMapper)
        {
        }

        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The TClient
        /// </returns>
        public override CustomClient Map(SimpleClient source)
        {
            var entity = base.Map(source);

            entity.AppId = (int)source.DataBag["AppId"];

            return entity;
        }
    }
}