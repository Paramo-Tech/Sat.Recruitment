using Sat.Recruitment.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Contracts.BonusCalculation
{
    internal interface IBonusCalculation
    {
        /// <summary>
        /// Calculates the bonus acording to the usertype
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>money + bonus</returns>
        public decimal CalculateBonus(User user);
    }
}
