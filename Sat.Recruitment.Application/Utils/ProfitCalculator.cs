namespace Sat.Recruitment.Application.Utils
{
    public abstract class ProfitCalculator
    {
        protected decimal _baseMoney = 100;
        protected decimal Profit = 0;
        protected decimal _percentage = 0;

        public ProfitCalculator() {
            SetBasePercentage();
        }

        public abstract void SetBasePercentage();
        
        public virtual decimal CalculateProfit(decimal money)
        {
            return money + Profit;
        }
    }
}
