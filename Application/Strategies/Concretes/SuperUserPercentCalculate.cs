namespace Application
{
    public class SuperUserPercentCalculate : IUserPercentCalculate
    {
        public double GetPercentage(decimal moneyToCalculate)
        {
            var percentage = 0.0;
            if (moneyToCalculate > 100)
            {
                percentage = 0.20;
            }
            return percentage;
        }
    }
}