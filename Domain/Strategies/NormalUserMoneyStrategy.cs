using Domain.Enums;
using System;

namespace Domain.Strategies
{
    public class NormalUserMoneyStrategy : IUserMoneyStrategy
    {
        public decimal CalculateUserMoneyAmount(decimal money)
        {
            decimal result = money;
            if (money > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                //If new user is normal and has more than USD100
                var gif = money * percentage;
                result = money + gif;
            }
            else
            {
                if (money <= 100 && money > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gif = money * percentage;
                    result = money + gif;
                }
            }

            return result;
        }
    }
}
