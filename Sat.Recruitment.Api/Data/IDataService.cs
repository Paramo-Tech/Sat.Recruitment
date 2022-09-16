using Sat.Recruitment.Api.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Data
{
    public interface IDataService
    {
        List<User> GetUsers();
        void Save(User user);
        bool Exists(Predicate<User> p);
        User GetUserBy(Predicate<User> p);
    }
}
