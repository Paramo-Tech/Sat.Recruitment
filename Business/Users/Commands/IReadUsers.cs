using Domain;
using System.Collections.Generic;

namespace Business.Users.Commands
{
    public interface IReadUsers
    {
        IEnumerable<User> GetUsers();
    }
}