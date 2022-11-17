using System;

namespace Sat.Recruitment.Api.Services
{
    public class GiftPremiumUser : IGift
    {
        public decimal CalcGift(decimal Money)
        {
            return Convert.ToDecimal(2);
        }
    }
}
