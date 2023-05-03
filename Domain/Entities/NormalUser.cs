using Domain.Enums;
using Domain.Events;
using System;


namespace Domain.Entities
{
    public class NormalUser : ICalculateMoney
    {
        //open to its extension closed to its modification
      
        public decimal CalculateAllocationToUser(decimal money)
        {
            if (money > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                //If new user is normal and has more than USD100
                var gif = money * percentage;
                money = money + gif;
            }
            if (money < 100)
            {
                if (money > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gif = money * percentage;
                    money = money + gif;
                }
            }

            return money;
        }
    }
}
