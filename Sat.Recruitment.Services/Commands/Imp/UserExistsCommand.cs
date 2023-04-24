using Sat.Recruitment.DTOs.Models;
using Sat.Recruitment.DTOs.Requests;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Services.Commands.Imp
{
    public class UserExistsCommand : IUserExistsCommand
    {
        public bool Execute(IEnumerable<User> users, UserCreateRequest request)
        {
            return users.Any(user =>
                user.Email == request.Email ||
                user.Phone == request.Phone ||
                (user.Name.ToLower() == request.Name.ToLower() && user.Address.ToLower() == request.Address.ToLower()));
        }
    }
}