namespace Sat.Recruitment.Application.Services.GifCalculator.Strategy
{
    public class SuperUserGifCalculator : IUserGifCalculator
    {
        private const decimal multiplier = 2M;

        public decimal Calculate(decimal money)
        {
            var result = money;
            if (money > 100)
            {
                result += (money * multiplier);
            }

            return result;
        }
    }
}
