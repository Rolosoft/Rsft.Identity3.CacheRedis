// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedToken.cs" company="Email Hippo Ltd">
//   © Email Hippo Ltd
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
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