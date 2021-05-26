using DataAccess;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Users.Commands
{
    public class ReadUsers : IReadUsers
    {
        private readonly IUserDataAccess userDataAcess;

        public ReadUsers()
        {
            userDataAcess = new UserDataAccess();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await userDataAcess.GetUsersAsync();
        }
    }
}
