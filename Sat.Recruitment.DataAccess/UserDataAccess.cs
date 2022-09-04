using Sat.Recruitment.Infrastructure.Interfaces.DataAccess;
using Sat.Recruitment.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.DataAccess
{
    public class UserDataAccess:EntityBase<User>, IUserDataAccess
    {
    }
}
