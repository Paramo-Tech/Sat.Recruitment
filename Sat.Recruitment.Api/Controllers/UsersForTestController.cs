using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    public class UsersForTestController:UsersController
    {
        ICollection<User> _mockedUsers;
        public UsersForTestController(ICollection<User> mockedUsers):base(null)
        {
            _mockedUsers = mockedUsers;

        }
        protected override void LoadUsers()
        {
            foreach (var usr in _mockedUsers)
            {
                _users.Add(usr);
            }
        }

        public ICollection<User> Users { get { return _users; } }
    }
}
