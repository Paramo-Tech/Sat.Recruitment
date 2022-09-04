using Sat.Recruitment.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Infrastructure.Interfaces.Bussiness
{
    public interface IUserBussiness
    {
        void CreateUser(User newUser);
    }
}
