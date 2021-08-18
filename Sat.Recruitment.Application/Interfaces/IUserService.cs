
using System.Threading.Tasks;
using Sat.Recruitment.Application.Core;
using Sat.Recruitment.Application.Dtos;

namespace Sat.Recruitment.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<object>> CreateUser(CreateUserDto userDto);
    }
}
