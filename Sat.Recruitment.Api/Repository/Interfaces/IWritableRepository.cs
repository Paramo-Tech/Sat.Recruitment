using Sat.Recruitment.Api.Models.Interfaces;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Repository.Interfaces
{
    public interface IWritableRepository<T> where T : IIdentifiable
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void Update(T entity);
    }
}
