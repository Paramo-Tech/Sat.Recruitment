using Sat.Recruitment.Api.Domain.Contracts;
using Sat.Recruitment.Domain;

namespace Sat.Recruitment.Api.Domain.Services.Contracts
{
    public interface IUserBuilderDirectorService
    {
        UserModel GetResult();
        void PrepareBuilder(IUserModel dto);
    }
}