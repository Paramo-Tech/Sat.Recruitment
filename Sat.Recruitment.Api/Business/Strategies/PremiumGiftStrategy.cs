using Sat.Recruitment.Api.Business.Entities;

namespace Sat.Recruitment.Api.Business.Strategies
{
    public class PremiumGiftStrategy : BaseStrategy, IGiftStrategy
    {
        public void ApplyGift(User user)
        {
            user.Money += CalculateGift(user.Money, 2);
        }
    }
}
