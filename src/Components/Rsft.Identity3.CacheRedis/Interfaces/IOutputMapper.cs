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

// <copyright file="IInputMapper.cs" company="Rolosoft Ltd">
// Copyright (c) Rolosoft Ltd. All rights reserved.
// </copyright>
namespace Rsft.Identity3.CacheRedis.Interfaces
{
    /// <summary>
    /// The Config Mapper Interface
    /// </summary>
    /// <typeparam name="TSimple">The type of the simple.</typeparam>
    /// <typeparam name="TComplex">The type of the complex.</typeparam>
    public interface IOutputMapper<in TSimple, out TComplex>
    {
        /// <summary>
        /// Maps the specified simple.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The TComplex
        /// </returns>
        TComplex Map(TSimple source);
    }
}