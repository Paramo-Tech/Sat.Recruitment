using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repositories
{
    public interface IUserRepository
    {
        void CreateUser(User user);

        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> IsUserDuplicated(User User);
    }
}
