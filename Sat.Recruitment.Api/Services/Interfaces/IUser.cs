using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Api.Helpers;
using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.Interfaces
{
    public interface IUser
    {
        Task<IList<User>> GetAllAsync();
        Task<User> GetByIdAsync();
        Task<Result> InsertAndSaveAsync(userCreateDTO user);
        Task<Result> UpdateAndSaveAsync(userCreateDTO user, int id);
        Task<Result> DeleteAsync();

    }
}
