using Sat.Recruitment.Api.Services.DataObjects;

namespace Sat.Recruitment.Api.Services.Contracts
{
    public interface IUserService
    {
        (bool isDuplicated, string resultMessage) CreateUser(CreateUserDto dto);
    }
}