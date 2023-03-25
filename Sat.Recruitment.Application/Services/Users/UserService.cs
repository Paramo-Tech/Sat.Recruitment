using FluentValidation;
using Sat.Recruitment.Application.Utils;
using Sat.Recruitment.DataAccess.Contract.Users;
using Sat.Recruitment.Domain.Contract.Users;
using Sat.Recruitment.Domain.Models.Enum;
using Sat.Recruitment.Domain.Models.Users;
using Sat.Recruitment.Domain.Results;

namespace Sat.Recruitment.Application.Services.Users
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly IUserDataAccess _userDataAccess;
        public UserService(IUserDataAccess userDataAccess, IValidator<User> validator) : base(userDataAccess, validator)
        {
            _userDataAccess = userDataAccess;
        }

        /// <summary>
        /// Add the user in the DB if passes the validations (fields and duplicates)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override async Task<ExecutionResult> AddItemAsync(User user)
        {
            user.Money = GetUserProfit(user.UserType, user.Money);
            return await base.AddItemAsync(user);
        }

        /// <summary>
        /// Checks if all filds are OK and check if exists duplicates in the DB.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override async Task<ExecutionResult> ValidateItemAsync(User user)
        {
            var baseValidation = await base.ValidateItemAsync(user);
            if (baseValidation.IsSuccess)
            {
               return  await _userDataAccess.CheckDuplicates(user);
            }

            return new ExecutionResult() { };
        }

        /// <summary>
        /// Returns the initial money + a profit.
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public decimal GetUserProfit(EUserType userType, decimal money)
        {
            /*
                The original code is not validating if UserType is null.
                What happens if this field is null? I'll return the original amount
             */

            var profit = 0m;
            switch (userType)
            {
                case EUserType.Normal:
                    profit = new NormalUserProfitCalculator().CalculateProfit(money);
                    break;
                case EUserType.SuperUser:
                    profit = new SuperUserProfitCalculator().CalculateProfit(money);
                    break;
                case EUserType.Premium:
                    profit = new PremiumUserProfitCalculator().CalculateProfit(money);
                    break;
                default
                    :
                    return money;
            }

            return profit;
        }
    }
}
