using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserDbContext
    {
        bool Create(UserDomain user);
        bool Update(UserDomain user);
        bool Delete(int id);
    }
}
