namespace Sat.Recruitment.Services.Domain.MoneyIncrease.Imp
{
    public class CalculateMoneyIncreaseSuperUser : ICalculateMoneyIncrease
    {
        private const double HighMoneyBonusLimit = 100;
        private const double SuperUserMoreThan100Percentage = 0.2;

        public double CalculateMoneyIncrease(double money)
        {
            return money > HighMoneyBonusLimit
                ? money += money * SuperUserMoreThan100Percentage
                : money;
        }
    }
}