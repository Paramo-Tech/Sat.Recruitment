using Sat.Recruitment.Core.UseCases;

namespace Sat.Recruitment.Core.Adapters
{
    public class PremiumUserBonusCalculator : IUserBonusCalculator
    {
        public decimal CalculateBonus(decimal money)
        {
            return money > 100 ? money * 2 : 0;
        }
    }
}
