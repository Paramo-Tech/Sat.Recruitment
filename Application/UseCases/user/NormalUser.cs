using Application.UseCases.user.interfacesBussiness;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.user
{
    public class NormalUser : Domain.Entities.UserDomain, IAllocationMoneyToUser
    {
        //open to its extension closed to its modification
        public NormalUser(decimal money)
        {

        }

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
