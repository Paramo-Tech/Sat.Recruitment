using Sat.Recruitment.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Common.Interfaces.Persistance
{
    public interface IRepositoryBase<T> where T : class
    {
        public Task<Result> AddAsync(T user);

        public Task<List<T>> GetAllAsync();
    }
}
