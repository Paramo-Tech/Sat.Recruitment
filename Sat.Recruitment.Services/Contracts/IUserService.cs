using System.Threading.Tasks;
using Sat.Recruitment.Services.DataObjects;

namespace Sat.Recruitment.Services.Contracts
{
    public interface IUserService
    {
        Task<(bool duplicated,  string resultMessage)> CreateUser(CreateUserModelDto modelDto);
    }
}