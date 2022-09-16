using Sat.Recruitment.Api.BusinessLogic.Model;
using System;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.BusinessLogic
{
    public interface IApplicationBL
    {
        public List<User> GetUsers();
        public void SaveUser(User user);
        User GetUser(Predicate<User> p);
    }
}
