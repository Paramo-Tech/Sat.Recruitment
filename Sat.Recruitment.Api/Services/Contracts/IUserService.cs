using System.Threading.Tasks;
using Sat.Recruitment.Api.Services.DataObjects;

namespace Sat.Recruitment.Api.Services.Contracts
{
    public interface IUserService
    {
        Task<(bool duplicated,  string resultMessage)> CreateUser(CreateUserModelDto modelDto);
    }
}