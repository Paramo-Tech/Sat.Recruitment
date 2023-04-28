using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Entities;

namespace Sat.Recruitment.Api.Strategies
{
    public class PremiumGiftStrategy : BaseStrategy, IGiftStrategy
    {
        public void ApplyGift(User user)
        {
            user.Money += CalculateGift(user.Money, 2);
        }
    }
}
