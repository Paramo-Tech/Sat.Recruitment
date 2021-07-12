using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Strategies
{
    public class SuperUserMoneyStrategy : IUserMoneyStrategy
    {
        public decimal CalculateUserMoneyAmount(decimal money)
        {
            decimal result;
            var percentage = Convert.ToDecimal(0.20);
            var gif = money * percentage;
            result = money + gif;
            return result;
        }
    }
}
