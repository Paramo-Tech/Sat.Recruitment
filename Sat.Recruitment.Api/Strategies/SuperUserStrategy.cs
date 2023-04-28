using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Entities;

namespace Sat.Recruitment.Api.Strategies
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
