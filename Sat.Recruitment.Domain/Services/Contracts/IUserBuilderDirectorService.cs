using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Contracts;

namespace Sat.Recruitment.Api.Domain.Services.Contracts
{
    public interface IUserBuilderDirectorService
    {
        User GetResult();
        void PrepareBuilder(IUserModel dto);
    }
}