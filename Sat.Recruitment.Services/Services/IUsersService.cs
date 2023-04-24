using Sat.Recruitment.DTOs.Requests;
using Sat.Recruitment.DTOs.Responses;
using System.Threading.Tasks;

namespace Sat.Recruitment.Services.Services
{
    public interface IUsersService
    {
        Task<UserCreateResponse> Create(UserCreateRequest request);
    }
}
