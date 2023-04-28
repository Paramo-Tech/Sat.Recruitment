using Sat.Recruitment.Api.Business.Entities;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Business.Services
{
    public interface IUserService
    {
        //Task<User> Update(User user);
        //Task<User> Delete(User user);
        Task<bool> Create(User user);
    }
}
