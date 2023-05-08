namespace Sat.Recruitment.Application.Services.GifCalculator.Strategy
{
    public class NormalUserGifCalculator : IUserGifCalculator
    {
        private const decimal lowPercentage = 0.12M;
        private const decimal highPercentage = 0.8M;

        public decimal Calculate(decimal money)
        {
            decimal result = money;
            if (money > 100)
            {
                result += money * lowPercentage;
            }
            else
            {
                if (money > 10)
                {
                    result += money * highPercentage;
                }
            }

            return result;
        }
    }
}
