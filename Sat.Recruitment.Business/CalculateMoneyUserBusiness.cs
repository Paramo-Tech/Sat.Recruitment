using Sat.Recruitment.Business.Contracts;
using Sat.Recruitment.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Business
{
    public class CalculateMoneyNormalUser : ICalculateMoneyUserBusiness
    {
        public void CalculateMoney(User user)
        {
            if (user.Money > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                var gif = user.Money * percentage;
                user.Money += gif;
            }
            if (user.Money < 100)
            {
                if (user.Money > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gif = user.Money * percentage;
                    user.Money += gif;
                }
            }

        }
    }

    public class CalculateMoneySuperUser : ICalculateMoneyUserBusiness
    {
        public void CalculateMoney(User user)
        {
            if (user.Money > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gif = user.Money * percentage;
                user.Money += gif;
            }
        }
    }

    public class CalculateMoneyPremiumUser : ICalculateMoneyUserBusiness
    {
        public void CalculateMoney(User user)
        {
            if (user.Money > 100)
            {
                var gif = user.Money * 2;
                user.Money += gif;
            }
        }
    }
}
