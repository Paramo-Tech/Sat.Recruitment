using Sat.Recruitment.Core.Enums;

namespace Sat.Recruitment.Core.Abstractions.BusinessFeatures.GiftByUserType
{
    /// <summary>
    /// Mediator between an interested entity in obtaining the corresponding gift
    /// amount for a User based on their UserType and initial amount of money,
    /// and the different strategies used for each UserType.
    /// </summary>
    public interface IGiftByUserTypeMediator
    {
        /// <summary>
        /// Given a UserType and an amount of money, it will return the
        /// corresponding gift amount
        /// </summary>
        decimal GetGiftByUserType(UserType? userType, decimal initialMoney);
    }
}