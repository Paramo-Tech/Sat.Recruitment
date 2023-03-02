using Sat.Recruitment.Api.Models.Interfaces;
using System;

namespace Sat.Recruitment.Api.Models.Users
{
    public class NormalUser : BasicUser
    {


        public override decimal Gift => GetGiftAmount();

        private decimal GetGiftAmount()
        {
            if (Money>100)
            {
                return Money * 0.12m;
            }
            else if (Money>10)
            {
                return Money * 0.8m;
            }
            return 0;
        }
    }
}
