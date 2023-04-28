using Sat.Recruitment.Api.Entities;

namespace Sat.Recruitment.Api.Strategies
{
    public class NormalGiftStrategy : BaseStrategy, IGiftStrategy
    {
        public void ApplyGift(User user)
        {
            if (user.Money > 100)
            {
                user.Money += CalculateGift(user.Money, 0.12m);
            }
            else if (user.Money > 10)
            {
                user.Money += CalculateGift(user.Money, 0.8m);
            }
        }
    }
}
