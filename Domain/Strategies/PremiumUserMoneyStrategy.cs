using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Strategies
{
    public class PremiumUserMoneyStrategy : IUserMoneyStrategy
    {
        public decimal CalculateUserMoneyAmount(decimal money)
        {
            decimal result = money;

            if (money > 100)
            {
                var gif = money * 2;
                result = money + gif;
            }

            return result;
        }
    }
}
