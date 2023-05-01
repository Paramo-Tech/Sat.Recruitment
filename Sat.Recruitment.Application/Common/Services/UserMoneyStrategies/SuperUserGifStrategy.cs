using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Common.Services.UserMoneyStrategies;

public class SuperUserGifStrategy : IUserGifStrategy
{
    public string UserType => "SuperUser";

    public decimal Calculate(User user)
    {
        var money = user.Money;
        if (money > 100)
        {
            var percentage = Convert.ToDecimal(0.20);
            var gif = money * percentage;
            return money + gif;
        }

        return money;
    }
}