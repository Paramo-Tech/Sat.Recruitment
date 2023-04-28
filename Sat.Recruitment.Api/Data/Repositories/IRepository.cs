using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Data.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> Create(T entity);
    }
}
