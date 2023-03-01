using Sat.Recruitment.Infrastructure.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.DataAccess
{
    public abstract class EntityBase<TEntity> : IEntityBase<TEntity>
    {
        protected ICollection<TEntity> _items;
        public void CreateEntity(TEntity entity)
        {
            _items.Add(entity);
        }

        public TEntity GetSingleBy(Func<TEntity, bool> predicate)
        {
            return _items.FirstOrDefault(predicate);
        }
    }
}
