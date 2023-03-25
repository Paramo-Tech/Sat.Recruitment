namespace Sat.Recruitment.Application.Utils
{
    public class NormalUserProfitCalculator : ProfitCalculator
    {
        private decimal _minPercentage = 0.8m;

        public override decimal CalculateProfit(decimal money)
        {
            if(money > _baseMoney)
            {
                Profit = money * _percentage;
            }
            else if(money < _baseMoney)
            {
                Profit = money * _minPercentage;
            }

            money += Profit;

            return money;
        }

        public override void SetBasePercentage()
        {
            _percentage = 0.12m;
        }
    }
}
