using Sat.Recruitment.Api.Logic.Interface;
using System;

namespace Sat.Recruitment.Api.Logic.Entity
{    
    public class SuperUser : User
    {
        Ihelpers h = new Helpers();
        override
        public decimal setMoney(string money)
        {
            decimal percentage = 0;
            decimal dmoney = decimal.Parse(money);
            if (dmoney >= 100)
            {
                percentage = Convert.ToDecimal(0.20);
            }
            else
            {
                throw new InvalidOperationException("Invalid money");
            }
            return h.convertMoney(dmoney, percentage);
        }
    }
    
}
