using Sat.Recruitment.BLL.Dto;
using System.Threading.Tasks;

namespace Sat.Recruitment.BLL.interfaces
{
    public interface IUserService
    {
        Task<Result> CreateUser(CreateUserDTO user);
    }
}
