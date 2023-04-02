using Application.Contracts;
using System;

namespace Application.Services
{
    public class NormalGiftService : IGiftService
    {
        public string Type { get; set; } = "Normal";

        public decimal GetDiscount(decimal money)
        {
            if (money > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                //If new user is normal and has more than USD100
                var gif = money * percentage;
                return money + gif;
            }
            else
            {
                if (money > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gif = money * percentage;
                    return money + gif;
                }
            }

            return money;
        }
    }
}