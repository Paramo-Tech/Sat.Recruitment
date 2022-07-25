using Sat.Recruitment.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.BusinessLogic.ExternalServices
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> Get(Guid UserId);
        Task<bool> Save(User User);
    }
}