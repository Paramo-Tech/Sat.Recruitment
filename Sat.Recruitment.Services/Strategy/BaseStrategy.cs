using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Services.Strategy
{
    public abstract class BaseStrategy
    {
        protected readonly decimal _money;
        protected decimal? _baseIncrement = null;

        public BaseStrategy(decimal money)
        {
            _money = money;
        }

        public virtual decimal ProcessMoney()
        {
            return CalculateMoney(_money, _baseIncrement);
        }
        protected decimal CalculateMoney(decimal money, decimal? increment)
        {
            if (increment.HasValue)
                return money += money * (decimal) increment;
            else
                return money;
        }
    }
}
