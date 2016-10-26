// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomClientWrapper.cs" company="Email Hippo Ltd">
//   © Email Hippo Ltd
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Rsft.Identity3.CacheRedis.Tests.TestHelpers
{
    using IdentityServer3.Core.Models;

    /// <summary>
    /// The Custom Client Wrapper
    /// </summary>
    public sealed class CustomClientWrapper
    {
        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        public Client Client { get; set; }
    }
}