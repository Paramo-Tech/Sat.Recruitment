using Sat.Recruitment.Application.DTOs;
using Sat.Recruitment.Domain.Entities;
using System.Collections.Generic;

namespace Sat.Recruitment.Application.Interfaces.Services
{
    public interface IUserService
    {
        void CreateNewUser(User user);
        List<UserDTO> GetUsers();
    }
}
