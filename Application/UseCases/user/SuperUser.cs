using Application.UseCases.user.interfacesBussiness;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.user
{
    public class SuperUser : Domain.Entities.UserDomain, IAllocationMoneyToUser
    {
        public SuperUser()
        {

        }

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
