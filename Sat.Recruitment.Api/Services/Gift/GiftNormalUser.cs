using System;

namespace Sat.Recruitment.Api.Services
{
    public class GiftNormalUser : IGift
    {
        public decimal CalcGift(decimal Money)
        {
            decimal percentage = 0;
            if (Money > 10)
            {
                if (Money > 100)
                    percentage = Convert.ToDecimal(0.12);
                else
                    percentage = Convert.ToDecimal(0.8);
            }
            return percentage;
        }
    }
}
