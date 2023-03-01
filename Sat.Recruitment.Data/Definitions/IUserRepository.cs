using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sat.Recruitment.Domain.Domains;

namespace Sat.Recruitment.Data.Definitions
{
    public interface IUserRepository
    {
        Task<Result> ProcessingNewUserAsync(User newUser);
        void ValidateErrors(User user, ref string errors);
        List<User> GetAll();
    }
}
