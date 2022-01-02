using Sat.Recruitment.Business;
using Sat.Recruitment.Business.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Business.Helpers
{
    public static class MoneyHelper
    {
        public static decimal UserPercentege(User user)
        {
            decimal percentage = 0;
            switch (user.UserType)
            {
                case UserType.Normal:
                    if (user.Money > 100)
                    {
                        //If new user is normal and has more than USD100
                        percentage = Convert.ToDecimal(0.12);
                    }
                    else
                    {
                        if (user.Money > 10)
                        {
                            percentage = Convert.ToDecimal(0.8);
                        }
                    }
                    break;

                case UserType.SuperUser:
                    if (user.Money > 100)
                    {
                        percentage = Convert.ToDecimal(0.20);
                    }
                    break;

                case UserType.Premium:
                    percentage = 2;

                    break;

            }

            return percentage;

        }
        public static decimal ParseMoney(string money)
        {
            return decimal.TryParse((string)money, out decimal result) ? result : -1;
        }
    }
}
