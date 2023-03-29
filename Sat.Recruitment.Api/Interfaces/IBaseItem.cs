using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Interfaces
{
    public interface IBaseItem<T> where T : class
    {
        public Task<Result> AddAsync(T item);
        public Task<List<User>> GetAllAsync();
    }
}
