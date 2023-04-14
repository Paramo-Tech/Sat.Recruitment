using Sat.Recruitment.Domain.Contracts.BonusCalculation;
using Sat.Recruitment.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.BonusCalculation
{
    public class BonusCalculationUserNormal : IBonusCalculation
    {   
        public decimal CalculateBonus(User user)
        {
            decimal percentageToIncreaseMoney = 0;
            if (user.Money > 100)
            {   //If new user is normal and has more than USD100
                percentageToIncreaseMoney = Convert.ToDecimal(0.12);

            }
            else if (user.Money <= 100 && user.Money > 10)
            {
                percentageToIncreaseMoney = Convert.ToDecimal(0.8);

            }

            var bonus = user.Money * percentageToIncreaseMoney;
            return user.Money + bonus;
        }
    }
}
