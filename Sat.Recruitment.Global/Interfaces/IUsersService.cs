using Sat.Recruitment.Global.WebContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Global.Interfaces
{
    public interface IUsersService
    {
        /// <summary>
        /// Retrieve users from file or cache if exists
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetUsers();

        /// <summary>
        /// Process user email and money based on his type an salary
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        User ProcessUser(User newUser);
    }
}
