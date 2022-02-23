using Sat.Recruitment.Core.Abstractions.BusinessFeatures.GiftByUserType;
using System;

namespace Sat.Recruitment.Core.BusinessRules.Features.GiftByUserType
{
    /// <summary>
    /// New Users receive a gift sum of money when they are registered in the system
    /// depending on the amount of money they start with, and the UserType that is
    /// assigned to them.
    /// 
    /// This strategy implementation is used to calculate the amount of gift for a
    /// SuperUser.
    /// </summary>
    public class SuperUserGiftStrategy : ISuperUserGiftStrategy
    {
        public decimal GetGift(decimal initialMoney)
        {
            decimal gift;
            decimal percentage = 0;

            // Assign the gift percentage depending on the established business rules
            if (initialMoney > 100)
            {
                percentage = Convert.ToDecimal(0.20);
            }

            // Calculate gift
            gift = initialMoney * percentage;

            return gift;
        }
    }
}
