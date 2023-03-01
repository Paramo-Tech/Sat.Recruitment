using System;

namespace Sat.Recruitment.Infrastructure.Interfaces.DataAccess
{
    public interface IEntityBase<TEntity>
    {
        void CreateEntity(TEntity entity);
        TEntity GetSingleBy(Func<TEntity, bool> predicate);
    }
}
