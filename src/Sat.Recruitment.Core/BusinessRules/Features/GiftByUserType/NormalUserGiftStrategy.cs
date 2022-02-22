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
    /// NormalUser.
    /// </summary>
    public class NormalUserGiftStrategy : INormalUserGiftStrategy
    {
        public decimal GetGift(decimal initialMoney)
        {
            decimal gift;
            decimal percentage = 0;

            // Assign the gift percentage depending on the established business rules
            if (initialMoney > 10 && initialMoney < 100)
            {
                percentage = Convert.ToDecimal(0.8); // TODO: Ask the business: is a gift of 80% correct?
            }
            else if (initialMoney > 100)
            {
                percentage = Convert.ToDecimal(0.12);
            }

            // Calculate gift
            gift = initialMoney * percentage;

            return gift;
        }
    }
}
