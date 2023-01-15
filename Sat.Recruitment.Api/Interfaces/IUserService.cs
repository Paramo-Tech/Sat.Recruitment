using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Interfaces
{
    public interface IUserService
    {
        Result createUser(User user);
        Task<Result> CreateUserAsync(User user);
        List<User> GetAllUsers();
        Task<List<User>> GetAllUsersAsync();
    }
}
