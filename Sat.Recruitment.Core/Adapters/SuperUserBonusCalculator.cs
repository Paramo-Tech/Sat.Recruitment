using Sat.Recruitment.Core.UseCases;

namespace Sat.Recruitment.Core.Adapters
{
    public class SuperUserBonusCalculator : IUserBonusCalculator
    {
        public decimal CalculateBonus(decimal money)
        {
            return money > 100 ? money * 0.20m : 0;
        }
    }
}
