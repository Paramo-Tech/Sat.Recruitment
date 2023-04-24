using Sat.Recruitment.DTOs.Models;
using Sat.Recruitment.DTOs.Requests;
using System.Collections.Generic;

namespace Sat.Recruitment.Services.Commands
{
    public interface IUserExistsCommand
    {
        bool Execute(IEnumerable<User> users,UserCreateRequest request);
    }
}