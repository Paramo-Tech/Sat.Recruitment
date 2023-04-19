using Sat.Recruitment.Core.Entities;
using System.Collections.Generic;

namespace Sat.Recruitment.Core.Interfaces
{
    public interface IUserRepository
    {
        bool AddUser(USER user);
        bool IsUserDuplicated(USER user);
        IEnumerable<USER> GetAllUsers();
    }
}
