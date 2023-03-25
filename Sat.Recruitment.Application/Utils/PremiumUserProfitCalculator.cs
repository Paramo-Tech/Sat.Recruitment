namespace Sat.Recruitment.Application.Utils
{
    public class PremiumUserProfitCalculator : ProfitCalculator
    {
        public override decimal CalculateProfit(decimal money)
        {
            if(money > _baseMoney)
            {
                Profit = money * _percentage;
            }

            return money + Profit;
        }

        public override void SetBasePercentage()
        {
            _percentage = 2;
        }
    }
}
