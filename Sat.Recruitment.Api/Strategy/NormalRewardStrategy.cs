using System;

namespace Sat.Recruitment.Api.Strategy
{
    public class NormalRewardStrategy : IRewardStrategy
    {
        public decimal AssignReward(decimal money)
        {
            decimal percentage = 0;
            if (money > 100)
                percentage = (decimal)0.12;
            else if (money < 100 && money > 10)
                percentage = (decimal)0.8;
            money += money * percentage;
            return money;
        }
    }
}
