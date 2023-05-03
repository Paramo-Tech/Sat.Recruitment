
using Domain.Entities;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class SuperUser : ICalculateMoney
    {

        public decimal CalculateAllocationToUser(decimal money)
        {
            if (money > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gif = money * percentage;
                money = money + gif;
            }
            return money;
        }
    }
}
