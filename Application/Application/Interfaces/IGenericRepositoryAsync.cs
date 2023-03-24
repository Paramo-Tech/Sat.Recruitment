using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<string> GetByIdJson(int id);
        Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> DeleteAsync(int entityID);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetFilterAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetFilterFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entity);
    }
}
