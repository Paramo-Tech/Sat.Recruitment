using Sat.Recruitment.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Services.Interfaces
{
    public interface IServiceBase<T>
    {
        Task<Result> AddItemAsync(T item);

        Task<List<T>> GetAllAsync();
    }
}
