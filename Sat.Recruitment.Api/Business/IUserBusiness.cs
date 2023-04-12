using Sat.Recruitment.Api.Models.Dtos;
using Sat.Recruitment.Api.Models;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Business
{
    public interface IUserBusiness
    {
        Task<ResultResponse> ProcessCreateUser(UserDto user);
    }
}
