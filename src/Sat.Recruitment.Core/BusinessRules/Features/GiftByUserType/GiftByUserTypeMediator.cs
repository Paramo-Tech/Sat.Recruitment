using Sat.Recruitment.Core.Abstractions.BusinessFeatures.GiftByUserType;
using Sat.Recruitment.Core.Enums;

namespace Sat.Recruitment.Core.BusinessRules.Features.GiftByUserType
{
    /// <summary>
    /// Mediator between an interested entity in obtaining the corresponding gift
    /// amount for a User based on their UserType and initial amount of money,
    /// and the different strategies used for each UserType.
    /// </summary>
    public class GiftByUserTypeMediator : IGiftByUserTypeMediator
    {
        private readonly INormalUserGiftStrategy _normalUserGiftStrategy;
        private readonly IPremiumUserGiftTrategy _premiumUserGiftTrategy;
        private readonly ISuperUserGiftStrategy _superUserGiftStrategy;

        public GiftByUserTypeMediator(INormalUserGiftStrategy normalUserGiftStrategy, IPremiumUserGiftTrategy premiumUserGiftTrategy, ISuperUserGiftStrategy superUserGiftStrategy)
        {
            this._normalUserGiftStrategy = normalUserGiftStrategy;
            this._premiumUserGiftTrategy = premiumUserGiftTrategy;
            this._superUserGiftStrategy = superUserGiftStrategy;
        }

        public decimal GetGiftByUserType(UserType? userType, decimal initialMoney)
        {
            // Switch between the different UserTypes to return the appropriate gift
            // amount, selecting the appropriate strategy.
            switch (userType)
            {
                case UserType.Normal:
                    return _normalUserGiftStrategy.GetGift(initialMoney);

                case UserType.SuperUser:
                    return _superUserGiftStrategy.GetGift(initialMoney);

                case UserType.Premium:
                    return _premiumUserGiftTrategy.GetGift(initialMoney);

                default:
                    return 0;
            }
        }
    }
}
