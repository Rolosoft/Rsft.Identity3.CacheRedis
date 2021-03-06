﻿// Copyright 2016 Rolosoft Ltd
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// <copyright file="ClaimMappers.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Mappers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Entities.Serialization;

    /// <summary>
    /// The Claim Mappers
    /// </summary>
    /// <seealso cref="BaseMapper{SimpleClaim, TClaim}" />
    internal sealed class ClaimMappers : BaseMapper<SimpleClaim, Claim>
    {
        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="Claim"/></returns>
        public override Claim ToComplexEntity(SimpleClaim source)
        {
            if (source == null)
            {
                return null;
            }

            return new Claim(source.Type, source.Value, source.ValueType, source.Issuer, source.OriginalIssuer);
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="SimpleClaim"/></returns>
        public override SimpleClaim ToSimpleEntity(object source)
        {
            if (source == null)
            {
                return null;
            }

            var claim = (Claim)source;

            return new SimpleClaim
            {
                ValueType = claim.ValueType,
                Issuer = claim.Issuer,
                OriginalIssuer = claim.OriginalIssuer,
                Properties = claim.Properties,
                Type = claim.Type,
                Value = claim.Value,
                DataBag = new Dictionary<string, object>()
            };
        }
    }
}