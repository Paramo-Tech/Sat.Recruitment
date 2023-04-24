namespace Sat.Recruitment.Services.Domain.MoneyIncrease.Imp
{
    public class CalculateMoneyIncreaseNormal : ICalculateMoneyIncrease
    {
        private const double HighMoneyBonusLimit = 100;
        private const double LowMoneyBonusLimit = 10;
        private const double NormalMoreThan100Percentage = 0.12;
        private const double NormalMoreThan10LessThan100Percentage = 0.8;

        public double CalculateMoneyIncrease(double money)
        {
            return money > HighMoneyBonusLimit
                ? money += money * NormalMoreThan100Percentage
                : money > LowMoneyBonusLimit
                    ? money += money * NormalMoreThan10LessThan100Percentage
                    : money;
        }
    }
}
