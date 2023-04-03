using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Data.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> Get();
        Task<User> Add(User entity);
    }
}
