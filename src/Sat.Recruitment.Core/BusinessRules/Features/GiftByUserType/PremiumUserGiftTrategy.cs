using Sat.Recruitment.Core.Abstractions.BusinessFeatures.GiftByUserType;

namespace Sat.Recruitment.Core.BusinessRules.Features.GiftByUserType
{
    /// <summary>
    /// New Users receive a gift sum of money when they are registered in the system
    /// depending on the amount of money they start with, and the UserType that is
    /// assigned to them.
    /// 
    /// This strategy implementation is used to calculate the amount of gift for a
    /// PremiumUser.
    /// </summary>
    internal class PremiumUserGiftTrategy : IPremiumUserGiftTrategy
    {
        public decimal GetGift(decimal initialMoney)
        {
            decimal gift;
            decimal percentage = 0;

            // Assign the gift percentage depending on the established business rules
            if (initialMoney > 100)
            {
                percentage = 2; // TODO: Ask the business: is a gift of 200% correct?
            }

            // Calculate gift
            gift = initialMoney * percentage;

            return gift;
        }
    }
}
