using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Create(UserDomain user);
        bool Update(UserDomain user);
        bool Delete(int id);
    }
}
