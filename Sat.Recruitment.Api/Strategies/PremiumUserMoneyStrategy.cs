using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Strategies
{
    public class PremiumUserMoneyStrategy : IUserMoneyStrategy
    {
        public decimal CalculateAdditionalMoney(decimal originalMoney)
        {
            decimal additionalMoney = 0;

            if (originalMoney > 100)
            {
                additionalMoney = originalMoney * 2;
            }

            return additionalMoney;
        }
    }

}
