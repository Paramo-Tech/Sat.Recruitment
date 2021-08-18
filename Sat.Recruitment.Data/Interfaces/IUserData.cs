using System.Collections.Generic;
using System.Threading.Tasks;
using Sat.Recruitment.Domain;

namespace Sat.Recruitment.Data.Interfaces
{
    public interface IUserData
    {
        Task<List<User>> GetUsersAsync();
    }
}
