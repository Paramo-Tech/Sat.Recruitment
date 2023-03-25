using Sat.Recruitment.Domain.Models.Enum;
using Sat.Recruitment.Domain.Models.Users;

namespace Sat.Recruitment.Domain.Contract.Users
{
    public interface IUserService : IServiceBase<User>
    {
        decimal GetUserProfit(EUserType userType, decimal money);
    }
}
