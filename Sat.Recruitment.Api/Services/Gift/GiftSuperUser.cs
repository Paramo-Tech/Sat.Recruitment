using System;

namespace Sat.Recruitment.Api.Services
{
    public class GiftSuperUser : IGift
    {
        public decimal CalcGift(decimal Money)
        {
            decimal percentage = 0;
            if (Money > 100)
                percentage = Convert.ToDecimal(0.20);

            return percentage;
        }
    }
}
