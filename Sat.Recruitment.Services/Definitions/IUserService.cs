using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sat.Recruitment.Domain.Domains;

namespace Sat.Recruitment.Services.Definitions
{
    public interface IUserService
    {
        Task<Result> ProcessingNewUser(User newUser);
    }
}
