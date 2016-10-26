// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoMapper.cs" company="Email Hippo Ltd">
//   © Email Hippo Ltd
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Rsft.Identity3.CacheRedis.Logic.Mappers
{
    internal sealed class NoMapper<TEntity> : BaseMapper<TEntity, TEntity>
        where TEntity : class
    {
        public override TEntity ToComplexEntity(TEntity source)
        {
            return source;
        }

        public override TEntity ToSimpleEntity(TEntity source)
        {
            return source;
        }
    }
}