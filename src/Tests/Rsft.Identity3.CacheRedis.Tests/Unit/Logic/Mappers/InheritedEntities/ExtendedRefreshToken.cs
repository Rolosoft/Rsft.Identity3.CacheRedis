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

// <copyright file="ExtendedRefreshToken.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Tests.Unit.Logic.Mappers.InheritedEntities
{
    using IdentityServer3.Core.Models;

    /// <summary>
    /// The Extended Token
    /// </summary>
    /// <seealso cref="IdentityServer3.Core.Models.Token" />
    public sealed class ExtendedRefreshToken : RefreshToken
    {
        /// <summary>
        /// Gets or sets the claim.
        /// </summary>
        public CustomProperty CustomProperty { get; set; }
    }
}