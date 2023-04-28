using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Entities;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public interface IUserService
    {
        //Task<User> Update(User user);
        //Task<User> Delete(User user);
        Task<bool> Create(User user);
    }
}
