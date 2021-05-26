using DataAccess;
using Domain;
using System.Collections.Generic;

namespace Business.Users.Commands
{
    public class ReadUsers : IReadUsers
    {
        private IUserDataAccess userDataAcess;

        public ReadUsers()
        {
            userDataAcess = new UserDataAccess();
        }

        public IEnumerable<User> GetUsers()
        {
            return userDataAcess.GetUsers();
        }
    }
}
