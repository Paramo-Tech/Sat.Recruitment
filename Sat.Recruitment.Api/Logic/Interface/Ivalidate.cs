using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Logic.Interface
{
    public interface Ivalidate
    {
        public bool validateDataUser(List<User>lstuser, User user);
    }
}
