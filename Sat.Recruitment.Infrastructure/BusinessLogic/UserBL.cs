using Sat.Recruitment.Application.BusinessLogic;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Infrastructure.Common;
using System;

namespace Sat.Recruitment.Infrastructure.BusinessLogic
{
    public class UserBL : IUserBL
    {
        public void ValidateGift(User user)
        {
            switch (user.UserType)
            {
                case UserEnumType.Normal:
                    user.Money = GetNormalGiftPercentage(user.Money);
                    break;
                case UserEnumType.Premium:
                    user.Money = GetPremiumGiftPercentage(user.Money);
                    break;
                case UserEnumType.SuperUser:
                    user.Money = GetSuperUserGiftPercentage(user.Money);
                    break;
                default:
                    break;
            }
        }
        public decimal GetNormalGiftPercentage(decimal money)
        {
            decimal percentage = 0;
            if (money > 100)
                percentage = Convert.ToDecimal(0.12);

            if (money < 100 && money > 10)
                percentage = Convert.ToDecimal(0.8);

            return money + (money * percentage);
        }

        public decimal GetPremiumGiftPercentage(decimal money)
        {
            decimal gif = 0;
            if (money > 100)
                gif = money * 2;

            return money + gif;
        }

        public decimal GetSuperUserGiftPercentage(decimal money)
        {
            decimal percentage = 0;
            if (money > 100)
                percentage = Convert.ToDecimal(0.20);

            return money + (money * percentage);
        }
        public void ValidateEmail(User user)
        {
            //Normalize email
            var aux = user.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            user.Email = string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
