using Sat.Recruitment.Domain.Entities;
using System.Collections.Generic;

namespace Sat.Recruitment.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void Save(User user);
        List<User> GetAll();
    }
}
