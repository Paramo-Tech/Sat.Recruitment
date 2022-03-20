using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Services.Strategy
{
    public static class CreateUserHelper
    {
        public static decimal HandlePercentage(UserTypeEnum userType, decimal money)
        {
            decimal percentage = 0;
            switch (userType)
            {
                case UserTypeEnum.NORMAL:
                    if (money > 100)
                    {
                        percentage = Convert.ToDecimal(0.12);
                    }
                    else if (money < 100 && money > 10)
                    {
                        percentage = Convert.ToDecimal(0.8);
                    }
                    money = SetMoney(money, percentage);
                    break;
                case UserTypeEnum.SUPERUSER:
                    if(money > 100)
                    {
                        percentage = Convert.ToDecimal(0.2);
                    }
                    money = SetMoney(money, percentage);
                    break;
                default:
                    break;
            }
            if(userType == UserTypeEnum.PREMIUM)
            {
                var gif = money * 2;
                money += gif;
            }
            return money;
        }

        private static decimal SetMoney(decimal money, decimal percentage)
        {
            var gif = money * percentage;
            return money + gif;
        }
    }
}
