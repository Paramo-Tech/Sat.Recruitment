using Sat.Recruitment.Domain.Contracts.BonusCalculation;
using Sat.Recruitment.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.BonusCalculation
{
    public class BonusCalculationPremiun : IBonusCalculation
    {
        public decimal CalculateBonus(User user)
        {
            decimal percentageToIncreaseMoney = 0;
            if (user.Money > 100)
            {
                percentageToIncreaseMoney = Convert.ToDecimal(2);
            }
            var bonus = user.Money * percentageToIncreaseMoney;
            return user.Money + bonus;
        }
    }
}
