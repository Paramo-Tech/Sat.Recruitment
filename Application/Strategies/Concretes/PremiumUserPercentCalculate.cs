namespace Application
{
    public class PremiumUserPercentCalculate : IUserPercentCalculate
    {
        public double GetPercentage(decimal moneyToCalculate)
        {
            var percentage = 0;
            if (moneyToCalculate > 100)
            {
                percentage = 2;
            }
            return percentage;
        }
    }
}