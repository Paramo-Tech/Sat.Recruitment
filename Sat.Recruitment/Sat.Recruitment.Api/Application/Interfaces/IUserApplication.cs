using System.Threading.Tasks;
using Sat.Recruitment.Api.Application.Request;
using Sat.Recruitment.Api.Services.Responses;

namespace Sat.Recruitment.Api.Application.Interfaces
{
    public interface IUserApplication
    {
        abstract Task<Result> AddUser(UserDTO request);
    }
}
