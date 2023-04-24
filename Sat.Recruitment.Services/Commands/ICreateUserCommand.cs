using Sat.Recruitment.DTOs.Requests;
using Sat.Recruitment.DTOs.Responses;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Commands
{
    public interface ICreateUserCommand
    {
        Task<UserCreateResponse> Execute(UserCreateRequest request);
    }
}