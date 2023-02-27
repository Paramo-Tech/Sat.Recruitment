using Sat.Recruitment.Api.Models.DTO;

namespace Sat.Recruitment.Api.Services.Interfaces
{
    public interface IUserService
    {
        void CreateUser(UserDTO dto);
    }
}
