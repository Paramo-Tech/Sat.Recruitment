using Domain;
using System.Collections.Generic;

namespace DataAccess
{
    public interface IUserDataAccess
    {
        IEnumerable<User> GetUsers();
    }
}