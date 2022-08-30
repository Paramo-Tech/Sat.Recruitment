using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Repository
{
    public interface IReadUsersFromFile
    {
        bool Exist(UserDto user);
    }
}
