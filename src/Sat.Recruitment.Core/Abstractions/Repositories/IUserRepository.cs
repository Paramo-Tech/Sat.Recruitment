using Sat.Recruitment.Core.DomainEntities;
using System;
using System.Collections.Generic;

namespace Sat.Recruitment.Core.Abstractions.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll(Func<User, bool> filter = null);
    }
}
