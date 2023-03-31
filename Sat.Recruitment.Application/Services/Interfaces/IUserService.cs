using Sat.Recruitment.Contracts.Results;
using Sat.Recruitment.Domain.Entities;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Services.Interfaces
{
    public interface IUserService : IServiceBase<User>
    {
        public Task<Result> ValidateUserData(User user);

        public Task<Result> ValidateUserRepo(User user);

        public decimal GetTypeUserMoneyGif(string userType, decimal money);

        public string GetNormalizeEmail(string email);
    }
}
