using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Services
{
    public class MoneyCalculatorService : IMoneyCalculatorService
    {
        public void CalculateMoney(User user)
        {
            var money = user.Money;
            decimal gif = 0;

            switch (user.UserType.ToLower())
            {
                case "normal":
                    // If new user is normal and has more than USD100
                    if (money > 100)
                        gif = money * 0.12m;
                    else if (money > 10)
                        gif = money * 0.8m;
                    break;
                case "superuser":
                    if (money > 100)
                        gif = money * 0.20m;
                    break;
                case "premium":
                    if (money > 100)
                        gif = money * 2;
                    break;
                default:
                    break;
            }

            user.Money = user.Money + gif;
        }
    }
}
