using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sat.Recruitment.Global.WebContracts;

namespace Sat.Recruitment.Global.Interfaces
{
    public interface IUsersService
    {
        List<User> UpdateUserList(User newUser);
    }
}
