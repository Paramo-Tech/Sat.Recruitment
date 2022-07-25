using Sat.Recruitment.Models.Models;
using Sat.Recruitment.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.BusinessLogic.ExternalServices
{
    public interface IUserSvc
    {
        Task<Result> Save(UserModel user);
        Task<List<User>> GetAll();
        Task<User> Get(Guid Id);
    }
}