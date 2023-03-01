using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Users.Commands
{
    public interface IReadUsers
    {
        Task<IEnumerable<User>> GetUsersAsync();
    }
}