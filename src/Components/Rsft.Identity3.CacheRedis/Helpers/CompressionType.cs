// <copyright file="CompressionType.cs" company="Rolosoft Ltd">
// © 2017, Rolosoft Ltd
// </copyright>

// Copyright 2014 Rolosoft Ltd
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
namespace Rsft.Identity3.CacheRedis.Helpers
{
    /// <summary>
    /// The compression type.
    /// </summary>
    public enum CompressionType
    {
        /// <summary>
        /// The none.
        /// </summary>
        None,

        /// <summary>
        /// The g zip.
        /// </summary>
        GZip,

        /// <summary>
        /// The deflate.
        /// </summary>
        Deflate
    }
}