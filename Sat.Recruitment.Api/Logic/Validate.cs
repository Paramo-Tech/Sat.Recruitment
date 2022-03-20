using Sat.Recruitment.Api.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Logic
{
    public class Validate : Ivalidate
    {
        public bool validateDataUser(List<User> lstuser, User user)
        {

            User vali=lstuser.Where(x => (x.Email == user.Email || x.Phone == user.Phone) || (x.Name == user.Name && x.Address == user.Address)).FirstOrDefault();

            return vali == null ? true:false;
        }
    }
}
