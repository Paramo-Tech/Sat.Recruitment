using Sat.Recruitment.Core.DomainEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.Abstractions.Services
{
    public interface IUserService
    {
        Task<User> Create(User newUser);
        Task<List<User>> GetAll(Func<User, bool> filter = null);
    }
}
