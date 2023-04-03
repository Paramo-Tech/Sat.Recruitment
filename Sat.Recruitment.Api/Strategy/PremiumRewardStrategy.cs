using System;

namespace Sat.Recruitment.Api.Strategy
{
    public class PremiumRewardStrategy : IRewardStrategy
    {
        public decimal AssignReward(decimal money)
        {
            decimal percentage = 0;
            if (money > 100)
                percentage = 2;
            money += money * percentage;
            return money;
        }
    }
}
