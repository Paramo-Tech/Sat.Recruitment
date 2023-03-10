using Sat.Recruitment.Domain.Dtos;

namespace Sat.Recruitment.Domain.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Insert(UserDto userDto);
        Task<string> GetUserTypeByEmail(string email);
        Task<bool> ValidateCredentials(string email, string password);
    }
}
