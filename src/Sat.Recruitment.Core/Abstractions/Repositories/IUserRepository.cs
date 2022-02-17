using Sat.Recruitment.Core.DomainEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll(Func<User, bool> filter = null);
    }
}
