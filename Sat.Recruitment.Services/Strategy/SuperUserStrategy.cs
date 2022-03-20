using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Services.Strategy
{
    public class SuperUserStrategy : BaseStrategy
    {
        public SuperUserStrategy(decimal money) : base(money) 
        {
            if (money > 100)
                _baseIncrement = Convert.ToDecimal(0.2);
        }
    }
}
