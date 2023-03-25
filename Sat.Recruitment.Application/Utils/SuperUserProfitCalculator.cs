namespace Sat.Recruitment.Application.Utils
{
    public class SuperUserProfitCalculator : ProfitCalculator
    {
        public override decimal CalculateProfit(decimal money)
        {

            if (money > _baseMoney)
            {
                Profit = money * _percentage;
            }

            money += Profit;

            return money;
        }

        public override void SetBasePercentage()
        {
            _percentage = 0.2m;
        }
    }
}
