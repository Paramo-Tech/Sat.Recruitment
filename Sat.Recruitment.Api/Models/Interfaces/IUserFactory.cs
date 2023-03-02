using Sat.Recruitment.Api.Models.DTO;

namespace Sat.Recruitment.Api.Models.Interfaces
{
    public interface IUserFactory
    {
        IUser CreateUser(UserDTO dto);
    }
}
