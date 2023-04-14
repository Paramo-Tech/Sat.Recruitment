using Sat.Recruitment.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();

        Task AddUser(User user);
    }
}
