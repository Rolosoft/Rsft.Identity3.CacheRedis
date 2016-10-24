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

// <copyright file="ClaimListMappers.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Logic.Mappers
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Security.Claims;
    using Entities.Serialization;
    using Interfaces;

    /// <summary>
    /// The Claim Mappers
    /// </summary>
    internal sealed class ClaimListMappers : BaseMapper<IEnumerable<SimpleClaim>, IEnumerable<Claim>>
    {
        /// <summary>
        /// The claim mapper
        /// </summary>
        private readonly IMapper<SimpleClaim, Claim> claimMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimListMappers"/> class.
        /// </summary>
        /// <param name="claimMapper">The claim mapper.</param>
        public ClaimListMappers(IMapper<SimpleClaim, Claim> claimMapper)
        {
            Contract.Requires(claimMapper != null);

            this.claimMapper = claimMapper;
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="Claim"/></returns>
        public override IEnumerable<Claim> ToComplexEntity(IEnumerable<SimpleClaim> source)
        {
            return source?.Select(r => this.claimMapper.ToComplexEntity(r)) ?? new List<Claim>();
        }

        /// <summary>
        /// To the simple entity.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="SimpleClaim"/></returns>
        public override IEnumerable<SimpleClaim> ToSimpleEntity(IEnumerable<Claim> source)
        {
            return source?.Select(r => this.claimMapper.ToSimpleEntity(r)) ?? new List<SimpleClaim>();
        }
    }
}