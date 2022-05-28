using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Contracts;

namespace Sat.Recruitment.Api.Domain.Services.Contracts
{
    public interface IUserBuilderDirectorService
    {
        UserModel GetResult();
        void PrepareBuilder(IUserModel dto);
    }
}