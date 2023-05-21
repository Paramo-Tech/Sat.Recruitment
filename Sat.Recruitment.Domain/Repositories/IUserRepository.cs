using Sat.Recruitment.Api.ViewModels;
using System.Collections.Generic;

namespace Sat.Recruitment.Domain.Respositories
{
    public interface IUserRepository
    {
        List<User> ReadUsers();
        void CreateUser(User user);
        User GetByEmail(string email);
    }
}
