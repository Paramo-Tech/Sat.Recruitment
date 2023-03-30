using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Interfaces
{
    public interface IUsersRepository : IBaseItem<User>
    {
        //Task<Result> AddAsync(User user);
        //Task<List<User>> GetAllAsync();
        Task<Result> TestAsync();
    }
}
