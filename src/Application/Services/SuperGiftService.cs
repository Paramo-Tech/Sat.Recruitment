using Application.Contracts;
using System;

namespace Application.Services
{
    public class SuperGiftService : IGiftService
    {
        public string Type { get; set; } = "SuperUser";

        public decimal GetDiscount(decimal money)
        {
            if (money > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gif = money * percentage;
                return money + gif;
            }

            return money;
        }
    }
}