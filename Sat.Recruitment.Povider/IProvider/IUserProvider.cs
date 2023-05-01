using Sat.Recruitment.Model.Shared;
using Sat.Recruitment.Model.User;

namespace Sat.Recruitment.Povider.IProvider
{
    public interface IUserProvider
    {
        Task<ResponseModel> CreateUser(UserModel user);
    }
}
