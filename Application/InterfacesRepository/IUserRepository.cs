using Domain.Entities;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Create(IUserType user);

        Task<IUserType> GetUserById(int id);

        Task<IUserType> GetUserByName(string name, string email);

        bool Update(UserDomain user);
        bool Delete(int id);
    }
}
