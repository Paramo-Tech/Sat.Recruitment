using System;

namespace Sat.Recruitment.Api.Strategy
{
    public class SuperUserRewardStrategy : IRewardStrategy
    {
        public decimal AssignReward(decimal money)
        {
            decimal percentage = 0;
            if (money > 100)
                percentage = Convert.ToDecimal(0.20);
            money += money * percentage;
            return money;
        }
    }
}
