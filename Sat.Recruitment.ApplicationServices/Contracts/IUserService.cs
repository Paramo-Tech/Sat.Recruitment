using System.Threading.Tasks;
using Sat.Recruitment.ApplicationServices.DataObjects;

namespace Sat.Recruitment.ApplicationServices.Contracts
{
    public interface IUserService
    {
        Task<(bool duplicated,  string resultMessage)> CreateUser(CreateUserModelDto modelDto);
    }
}