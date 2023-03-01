namespace Application
{
    public class NormalUserPercentCalculate : IUserPercentCalculate
    {
        public double GetPercentage(decimal moneyToCalculate)
        {
            var percentage = 0.0;
            if (moneyToCalculate > 100)
            {
                percentage = 0.12;
            }
            if (moneyToCalculate < 100 && moneyToCalculate > 10)
            {
                percentage = 0.8;
            }
            return percentage;
        }
    }
}