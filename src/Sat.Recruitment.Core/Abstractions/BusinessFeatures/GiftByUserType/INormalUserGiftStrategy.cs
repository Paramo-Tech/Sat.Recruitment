namespace Sat.Recruitment.Core.Abstractions.BusinessFeatures.GiftByUserType
{
    /// <summary>
    /// Interface used as Marker or Tagging Pattern, to simply specify that the class
    /// that implements it belongs to a family of functionality (UserTypeGiftStrategy),
    /// but that in turn it will be used specifically for a type (NormarUser in this case).
    /// </summary>
    public interface INormalUserGiftStrategy : IUserTypeGiftStrategy
    {
    }
}
