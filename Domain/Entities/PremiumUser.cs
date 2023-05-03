
using Domain.Events;

namespace Domain.Entities
{
    public class PremiumUser : ICalculateMoney
    {
        public decimal CalculateAllocationToUser(decimal money)
        {
            if (money > 100)
            {
                var gif = money * 2;
                money = money + gif;
            }
            return money;
        }
    }
}
