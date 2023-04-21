using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Strategies
{
    public class NormalUserMoneyStrategy : IUserMoneyStrategy
    {
        public decimal CalculateAdditionalMoney(decimal originalMoney)
        {
            decimal additionalMoney = 0;

            if (originalMoney > 100)
            {
                additionalMoney = originalMoney * 0.12m;
            }
            else if (originalMoney > 10)
            {
                additionalMoney = originalMoney * 0.8m;
            }

            return additionalMoney;
        }
    }

    
}
