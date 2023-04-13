using Sat.Recruitment.Business.Models;
using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Business.Contracts
{
    public interface IUserService
    {
        void ValidateErrors(UserModel user, ref string errors);
        void IsDuplicated(List<UserModel> users, UserModel userModel, ref string errors);
        ResultModel CreateUser(UserModel user);
        StreamReader ReadUsersFromFile();
    }
}
