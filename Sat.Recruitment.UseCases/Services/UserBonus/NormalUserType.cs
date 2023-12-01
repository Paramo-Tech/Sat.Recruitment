using Sat.Recruitment.UseCasesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.UseCases.Services.UserBonus
{
    public class NormalUserType : IUserTypeBonus
    {
        public decimal CalculateBonus(decimal money)
        {
            if (money > 100)
            {
                return money * 0.12m;
            }
            else if (money > 10 && money < 100)
            {
                return money * 0.8m;
            }
            return 0;
        }
    }
}
