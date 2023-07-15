using System.Threading.Tasks;
using Sat.Recruitment.Api.Application.Request;
using Sat.Recruitment.Api.Services.Responses;

namespace Sat.Recruitment.Api.Middleware.Interfaces
{
    public interface IUserServices
    {
        abstract Task<Result> AddUser(UserDTO request);
    }
}
