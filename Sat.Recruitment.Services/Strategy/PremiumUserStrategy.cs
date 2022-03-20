using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Services.Strategy
{
    public class PremiumUserStrategy : BaseStrategy
    {
        public PremiumUserStrategy(decimal money) : base(money) 
        {
            _baseIncrement = 2;
        }
       
    }
}
