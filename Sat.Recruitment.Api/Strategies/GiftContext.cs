using Sat.Recruitment.Api.Entities;

namespace Sat.Recruitment.Api.Strategies
{
    public class GiftContext
    {
        private readonly IGiftStrategy _giftStrategy;

        public GiftContext(IGiftStrategy giftStrategy)
        {
            _giftStrategy = giftStrategy;
        }

        public void ApllyGift(User user)
        {
            _giftStrategy.ApplyGift(user);
        }
    }
}
