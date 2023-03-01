using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IUserDataAccess
    {
        Task<IEnumerable<User>> GetUsersAsync();
    }
}