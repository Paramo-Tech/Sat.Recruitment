using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Common.Services.UserMoneyStrategies;

public class PremiumUserGifStrategy : IUserGifStrategy
{
    public string UserType => "Premium";

    public decimal Calculate(User user)
    {
        var money = user.Money;
        if (money > 100)
        {
            var gif = money * 2;
            return money + gif;
        }

        return money;
    }
}