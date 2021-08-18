
using System.Collections.Generic;
using System.Threading.Tasks;
using Sat.Recruitment.Application.Core;

namespace Sat.Recruitment.Application.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(int projectId);



    }
}
