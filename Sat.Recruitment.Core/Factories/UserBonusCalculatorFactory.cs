using Sat.Recruitment.Core.Adapters;
using Sat.Recruitment.Core.UseCases;
using System;

namespace Sat.Recruitment.Core.Factories
{
    public static class UserBonusCalculatorFactory
    {
        public static IUserBonusCalculator GetBonusCalculator(string userType)
        {
            return userType switch
            {
                "Normal" => new NormalUserBonusCalculator(),
                "SuperUserDto" => new SuperUserBonusCalculator(),
                "Premium" => new PremiumUserBonusCalculator(),
                _ => throw new ArgumentException($"Invalid user type: {userType}"),
            };
        }
    }
}
