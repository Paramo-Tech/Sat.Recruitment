namespace Sat.Recruitment.Core.Abstractions.BusinessFeatures.GiftByUserType
{
    /// <summary>
    /// New Users receive a gift sum of money when they are registered in the system
    /// depending on the amount of money they start with, and the UserType that is
    /// assigned to them.
    /// 
    /// This interface is used to represent different Gift strategies, depending on
    /// the UserType.
    /// </summary>
    public interface IUserTypeGiftStrategy
    {
        /// <summary>
        /// Given an initial amount of money, it returns the amount of gift according
        /// to the UserType this implementation is referred.
        /// </summary>
        decimal GetGift(decimal initialMoney);
    }
}
