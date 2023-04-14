using Sat.Recruitment.Application.Models;
using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Application.Contracts.Application
{
    public interface IUserService
    {
        ResultUser CreateUser(User user);
        List<User> GetUsers();
    }
}
