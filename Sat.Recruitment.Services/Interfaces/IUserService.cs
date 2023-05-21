using Sat.Recruitment.Api.ViewModels;
using Sat.Recruitment.Api.ViewModels;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUser(string email);
        Task<UserDTO>SaveUser(UserDTO userDTO);
    }
}
