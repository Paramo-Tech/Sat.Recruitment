using Sat.Recruitment.Business.Models;
using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Business.Contracts
{
    public interface IUserService
    {
        decimal AssignMoney(UserModel user);
        ResultModel CreateUser(UserModel user);
    }
}
