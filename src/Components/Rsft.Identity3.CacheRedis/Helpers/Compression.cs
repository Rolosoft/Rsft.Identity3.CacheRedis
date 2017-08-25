// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Compression.cs" company="Rolosoft Ltd">
// © 2017, Rolosoft Ltd
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.IO.Compression;
    using System.Text;
    using JetBrains.Annotations;

    /// <summary>
    /// Compression utilities.
    /// </summary>
    /// <remarks>
    /// Handles relatively small in-memory streams of data.
    /// </remarks>
    public static class Compression
    {
        /// <summary>
        /// The read buffer size
        /// </summary>
        private const int ReadBufferSize = 8192;

        /// <summary>
        /// Compresses data.
        /// </summary>
        /// <param name="data">The data to compress.</param>
        /// <param name="compressionType">Type of the compression (default is Deflate).</param>
        /// <param name="compressionLevel">The compression level (default is Optimal).</param>
        /// <returns>
        /// The <see cref="byte" /> array of compressed data.
        /// </returns>
        /// <exception cref="ArgumentNullException">data is null.</exception>
        public static byte[] Compress(
            [NotNull] this byte[] data,
            CompressionType compressionType = CompressionType.Deflate,
            CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            Contract.Requires(data != null);
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            /*No compression*/
            if (compressionType == CompressionType.None)
            {
                return data;
            }

            using (var stream = new MemoryStream())
            {
                /*Deflate compression*/
                if (compressionType == CompressionType.Deflate)
                {
                    using (var deflateStream = new DeflateStream(stream, compressionLevel))
                    {
                        deflateStream.Write(data, 0, data.Length);
                    }

                    return stream.ToArray();
                }

                /*GZip compression*/
                if (compressionType == CompressionType.GZip)
                {
                    using (var deflateStream = new GZipStream(stream, compressionLevel))
                    {
                        deflateStream.Write(data, 0, data.Length);
                    }

                    return stream.ToArray();
                }
            }

            /*Fallback, no compression*/
            return data;
        }

        /// <summary>
        /// Compress a string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="compressionType">Type of the compression (default is Deflate).</param>
        /// <param name="compressionLevel">The compression level (default is Optimal).</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">data is null</exception>
        public static string Compress(
            [NotNull] this string data,
            CompressionType compressionType = CompressionType.Deflate,
            CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            Contract.Requires(data != null);

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var byteArray = data.ToByteArray();
            if (byteArray != null)
            {
                var compress = byteArray.Compress(compressionType, compressionLevel);
                var s = compress.ToStringInternal();

                return s;
            }

            return null;
        }

        /// <summary>
        /// Decompresses data.
        /// </summary>
        /// <param name="data">The data to decompress.</param>
        /// <param name="compressionType">Type of the compression (default is Deflate).</param>
        /// <returns>
        /// The <see cref="byte" /> array of decompressed data.
        /// </returns>
        /// <exception cref="ArgumentNullException">data is null.</exception>
        public static byte[] Decompress(
            [NotNull] this byte[] data,
            CompressionType compressionType = CompressionType.Deflate)
        {
            Contract.Requires(data != null);

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (compressionType == CompressionType.None)
            {
                return data;
            }

            using (var outputStream = new MemoryStream())
            {
                using (var inputStream = new MemoryStream(data))
                {
                    /*Deflate*/
                    if (compressionType == CompressionType.Deflate)
                    {
                        using (var decompressionStream = new DeflateStream(inputStream, CompressionMode.Decompress))
                        {
                            var buffer = new byte[ReadBufferSize];
                            while (true)
                            {
                                var size = decompressionStream.Read(buffer, 0, buffer.Length);
                                if (size > 0)
                                {
                                    outputStream.Write(buffer, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            return outputStream.ToArray();
                        }
                    }

                    /*GZip*/
                    if (compressionType == CompressionType.GZip)
                    {
                        using (var decompressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
                        {
                            var buffer = new byte[ReadBufferSize];
                            while (true)
                            {
                                var size = decompressionStream.Read(buffer, 0, buffer.Length);
                                if (size > 0)
                                {
                                    outputStream.Write(buffer, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            return outputStream.ToArray();
                        }
                    }
                }
            }

            /*Default condition*/
            return data;
        }

        /// <summary>
        /// Decompress a string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="compressionType">Type of the compression (default is Deflate).</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">data is null.</exception>
        public static string Decompress(
            [NotNull] this string data,
            CompressionType compressionType = CompressionType.Deflate)
        {
            Contract.Requires(data != null);

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var fromBase64 = data.FromBase64();

            var decompress = fromBase64.Decompress(compressionType);

            var s = decompress.ToString(null);

            return s;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="encodingUsing">The encoding using.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public static string ToString(
            this byte[] input,
            Encoding encodingUsing,
            int index = 0,
            int count = -1)
        {
            if (input == null)
            {
                return string.Empty;
            }

            if (count == -1)
            {
                count = input.Length - index;
            }

            return encodingUsing.Check(new UTF8Encoding()).GetString(input, index, count);
        }

        /// <summary>
        /// The check.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="myObject">The my object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The generic T.
        /// </returns>
        /// <exception cref="ArgumentNullException">defaultValue</exception>
        private static T Check<T>(
            this T myObject,
            [NotNull] Func<T> defaultValue)
            where T : class
        {
            Contract.Requires(defaultValue != null);

            if (defaultValue == null)
            {
                throw new ArgumentNullException(nameof(defaultValue));
            }

            return myObject.Check(x => x != null, defaultValue);
        }

        /// <summary>
        /// The check.
        /// </summary>
        /// <typeparam name="T">Type to check.</typeparam>
        /// <param name="myObject">The my object.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The generic T.
        /// </returns>
        /// <exception cref="ArgumentNullException">predicate
        /// or
        /// defaultValue</exception>
        private static T Check<T>(
            this T myObject,
            [NotNull] Predicate<T> predicate,
            [NotNull] Func<T> defaultValue)
        {
            Contract.Requires(predicate != null);
            Contract.Requires(defaultValue != null);

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (defaultValue == null)
            {
                throw new ArgumentNullException(nameof(defaultValue));
            }

            return predicate(myObject) ? myObject : defaultValue();
        }

        /// <summary>
        /// The check.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="myObject">The my object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The generic T.
        /// </returns>
        private static T Check<T>(this T myObject, T defaultValue = default(T))
        {
            return myObject.Check(x => x != null, defaultValue);
        }

        /// <summary>
        /// The check.
        /// </summary>
        /// <typeparam name="T">The Type</typeparam>
        /// <param name="myObject">The my object.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        /// The generic T.
        /// </returns>
        /// <exception cref="ArgumentNullException">predicate</exception>
        private static T Check<T>(
            this T myObject,
            [NotNull] Predicate<T> predicate,
            T defaultValue = default(T))
        {
            Contract.Requires(predicate != null);

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return predicate(myObject) ? myObject : defaultValue;
        }

        /// <summary>
        /// The from base 64.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/> array.
        /// </returns>
        private static byte[] FromBase64(this string input)
        {
            return string.IsNullOrEmpty(input) ? new byte[0] : Convert.FromBase64String(input);
        }

        /// <summary>
        /// The to byte array.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="encodingUsing">The encoding using.</param>
        /// <returns>
        /// The <see cref="byte" /> arrary.
        /// </returns>
        private static byte[] ToByteArray(this string input, Encoding encodingUsing = null)
        {
            return string.IsNullOrEmpty(input) ? null : encodingUsing.Check(new UTF8Encoding()).GetBytes(input);
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        private static string ToStringInternal(
            this byte[] input,
            int index = 0,
            int count = -1)
        {
            if (count == -1)
            {
                count = input.Length - index;
            }

            return input == null ? string.Empty : Convert.ToBase64String(input, index, count);
        }
    }
}