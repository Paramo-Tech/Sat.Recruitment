using Sat.Recruitment.Application.Services.GifCalculator.Strategy;
using Sat.Recruitment.Domain.Enum;
using System;

namespace Sat.Recruitment.Application.Services.GifCalculator.Factory
{
    public class UserGifCalculatorFactory : IUserGifCalculatorFactory
    {
        public IUserGifCalculator CreateCalculator(UserType userType)
        {
            if (userType == UserType.SuperUser)
            {
                return new SuperUserGifCalculator();
            }
            else if (userType == UserType.Premium)
            {
                return new PremiumUserGifCalculator();
            }
            else if (userType == UserType.Normal)
            {
                return new NormalUserGifCalculator();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
