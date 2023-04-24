namespace Sat.Recruitment.Services.Domain.MoneyIncrease.Imp
{
    public class CalculateMoneyIncreasePremium : ICalculateMoneyIncrease
    {
        private const double PremiumMoreThan100Percentage = 2;
        private const double HighMoneyBonusLimit = 100;

        public double CalculateMoneyIncrease(double money)
        {
            return money > HighMoneyBonusLimit
                ? money += money * PremiumMoreThan100Percentage
                : money;
        }
    }
}