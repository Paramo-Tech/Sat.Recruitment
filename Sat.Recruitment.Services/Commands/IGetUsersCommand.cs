using Sat.Recruitment.DTOs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Commands
{
    public interface IGetUsersCommand
    {
        Task<List<User>> Execute();
    }
}
