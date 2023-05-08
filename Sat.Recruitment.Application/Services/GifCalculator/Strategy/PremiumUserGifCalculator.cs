using System;

namespace Sat.Recruitment.Application.Services.GifCalculator.Strategy
{
    public class PremiumUserGifCalculator : IUserGifCalculator
    {
        private const decimal percentage = 0.20M;

        public decimal Calculate(decimal money)
        {
            var result = money;
            if (money > 100)
            {
                result += money * percentage;
            }

            return result;
        }
    }
}
