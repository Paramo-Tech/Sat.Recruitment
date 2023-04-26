using Application.UseCases.user.interfacesBussiness;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.user
{
    public class Premium : Domain.Entities.UserDomain, IAllocationMoneyToUser
    {
        public Premium()
        {
        }

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
