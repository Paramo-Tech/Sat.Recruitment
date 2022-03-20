using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Services.Strategy
{
    public class NormalUserStrategy : BaseStrategy
    {
        public NormalUserStrategy(decimal money) : base(money) 
        {
            if(money > 100)
            {
                _baseIncrement = Convert.ToDecimal(0.12);
            }
            else if(money < 100 && money > 10)
            {
                _baseIncrement = Convert.ToDecimal(0.8);
            }
        }
    }
}
