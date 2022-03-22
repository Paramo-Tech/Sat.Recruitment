using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task<List<T>> GetAllAsync(CancellationToken token);
        List<T> GetAll();
        Task<T> AddAsync(T entity, CancellationToken token);
        T Add(T entity);
        Task<T> GetAsync(ulong id, CancellationToken token);
        T Get(ulong id);
        Task DeleteAsync(ulong id, CancellationToken token);
        Task<T> UpdateAsync(T entity, CancellationToken token);
        T Update(T entity);  
        void Delete(ulong id);
        Task AddRangeAsync(List<T> entities, CancellationToken token);
        void AddRange(List<T> entities);
        void DeleteRange(List<T> entities);
        Task DeleteRangeAsync(List<T> entities, CancellationToken token);

    }
}
