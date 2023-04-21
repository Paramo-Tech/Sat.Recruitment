using Sat.Recruitment.Api.Models;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Services
{
    public interface IUserService
    {
        bool Create(User entity);
        IList<User> GetAll();
    }
}
