using Sat.Recruitment.DTOs.Enums;
using Sat.Recruitment.Services.Domain.MoneyIncrease;

namespace Sat.Recruitment.Services.Commands.Imp
{
    public class CalculateMoneyIncreaseCommand : ICalculateMoneyIncreaseCommand
    {
        private readonly ICalculateMoneyIncreaseFactory calculateMoneyIncreaseFactory;

        public CalculateMoneyIncreaseCommand(ICalculateMoneyIncreaseFactory calculateMoneyIncreaseFactory)
        {
            this.calculateMoneyIncreaseFactory = calculateMoneyIncreaseFactory;
        }

        public double Execute(UserType userType, double money)
        {
            var moneyIncreaseFactory = calculateMoneyIncreaseFactory.GetInstance(userType);

            return moneyIncreaseFactory.CalculateMoneyIncrease(money);
        }
    }
}