using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(User user);
        Task<IEnumerable<User>> GetAllUser();
        Task<User> UpdateUser(User module);
    }
}
