using Sat.Recruitment.UseCasesAbstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.UseCases.Services.UserBonus
{
    public class SuperUserType : IUserTypeBonus
    {
        public decimal CalculateBonus(decimal money)
        {
            if (money > 100)
            {
                return money * 0.20m;
            }
            return 0;
        }
    }
}
