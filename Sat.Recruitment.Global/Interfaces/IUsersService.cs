using Sat.Recruitment.Global.WebContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Global.Interfaces
{
    public interface IUsersService
    {
        Task<List<User>> GetUserList();

        User ProcessUser(User newUser);
    }
}
