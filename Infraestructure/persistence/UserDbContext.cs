using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.persistence
{
    public class UserDbContext : IUserDbContext
    {
        public bool Create(UserDomain user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(UserDomain user)
        {
            throw new NotImplementedException();
        }
    }
}
