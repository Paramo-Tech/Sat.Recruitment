using Sat.Recruitment.Domain.Entities;
using System.Collections.Generic;

namespace Sat.Recruitment.Application.Contracts.Persistence
{
    public interface IUserRepository
    {
        List<User>GetUsers();
    }
}
