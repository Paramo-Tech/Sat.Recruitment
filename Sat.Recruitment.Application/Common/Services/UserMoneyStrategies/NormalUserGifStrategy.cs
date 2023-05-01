using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Common.Services.UserMoneyStrategies;

public class NormalUserGifStrategy : IUserGifStrategy
{
    public string UserType => "Normal";

    public decimal Calculate(User user)
    {
        var money = user.Money;
        if (money > 100)
        {
            var percentage = Convert.ToDecimal(0.12);
            var gif = money * percentage;
            return money + gif;
        }

        if (money < 100)
        {
            if (money > 10)
            {
                var percentage = Convert.ToDecimal(0.8);
                var gif = money * percentage;
                return money + gif;
            }
        }

        return money;
    }
}