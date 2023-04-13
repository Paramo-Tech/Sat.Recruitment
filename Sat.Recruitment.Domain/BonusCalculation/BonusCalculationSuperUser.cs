using Sat.Recruitment.Domain.Contracts.BonusCalculation;
using Sat.Recruitment.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.BonusCalculation
{
    public class BonusCalculationSuperUser : IBonusCalculation
    {
        public decimal CalculateBonus(User user)
        {
            decimal percentageToIncreaseMoney = 0;
            if (user.Money > 100)
            {
                percentageToIncreaseMoney = Convert.ToDecimal(0.20);
            }

            var bonus = user.Money * percentageToIncreaseMoney;
            return user.Money + bonus;
        }
    }
}
