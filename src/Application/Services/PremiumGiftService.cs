using Application.Contracts;

namespace Application.Services
{
    public class PremiumGiftService : IGiftService
    {
        public string Type { get; set; } = "Premium";

        public decimal GetDiscount(decimal money)
        {
            if (money > 100)
            {
                var gif = money * 2;
                return money + gif;
            }

            return money;
        }
    }
}