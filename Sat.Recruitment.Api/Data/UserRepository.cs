using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Data
{
    public class UserRepository : IUsersRepository
    {
        public Task<Result> AddAsync(User user)
        {
            DataContext.UserList.Add(user);
            return Task.FromResult(new Result { IsSuccess = true, Errors = "User Created" });
        }

        public Task<List<User>> GetAllAsync()
        {
            return Task.FromResult(DataContext.UserList);
        }

    }
}
