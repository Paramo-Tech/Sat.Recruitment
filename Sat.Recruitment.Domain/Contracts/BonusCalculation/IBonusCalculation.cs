using Sat.Recruitment.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Contracts.BonusCalculation
{
    internal interface IBonusCalculation
    {
        public decimal CalculateBonus(User user);
    }
}
