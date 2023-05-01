using Sat.Recruitment.Model.Shared;
using Sat.Recruitment.Model.User;

namespace Sat.Recruitment.Services.IServices
{
    public interface IService
    {
        Task<List<UserModel>> GetUsers();
        Task<ResponseModel> AddUser(UserModel user);
    }
}
