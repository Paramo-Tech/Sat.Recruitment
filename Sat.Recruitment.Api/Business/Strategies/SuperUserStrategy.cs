using Sat.Recruitment.Api.Business.Entities;

namespace Sat.Recruitment.Api.Business.Strategies
{
    public class SuperUserStrategy : BaseStrategy, IGiftStrategy
    {
        public void ApplyGift(User user)
        {
            if (user.Money > 100)
            {
                user.Money += CalculateGift(user.Money, 0.20m);
            }
        }
    }
}
