using Sat.Recruitment.Core.UseCases;

namespace Sat.Recruitment.Core.Adapters
{
    public class NormalUserBonusCalculator : IUserBonusCalculator
    {
        public decimal CalculateBonus(decimal money)
        {
            decimal bonus = 0;
            if (money > 100)
            {
                bonus = money * 0.12m;
            }
            else if (money > 10)
            {
                bonus = money * 0.08m;
            }
            return bonus;
        }
    }
}
