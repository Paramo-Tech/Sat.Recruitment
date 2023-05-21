using Sat.Recruitment.Api.ViewModels;
using Sat.Recruitment.Domain.Enums;
using System;

namespace Sat.Recruitment.Services
{
    internal class GiftCalculator
    {
        public static void SetMoney(decimal money, User user)
        {
            decimal percentage = 0;
            if (user.UserType == UserType.Normal)
            {
                if (money > 100)
                {
                    percentage = Convert.ToDecimal(0.12);//12%
                }
                else if (money > 10)
                {
                    percentage = Convert.ToDecimal(0.8);//80%
                }
            }
            else if (money > 100)
            {
                if (user.UserType == UserType.SuperUser)
                {
                    percentage = Convert.ToDecimal(0.20);//%20
                }
                else if (user.UserType == UserType.Premium)
                {
                    percentage = 2;//200%
                }
            }
            if (percentage > 0)
            {
                var gift = money * percentage;
                user.Money += gift;
            }
        }
    }
}
