using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Interfaces
{
    public interface IServiceBase<T>
    {
        Task<Result> AddItemAsync(T item);
        Task<List<User>> GetAllAsync();
    }
}
