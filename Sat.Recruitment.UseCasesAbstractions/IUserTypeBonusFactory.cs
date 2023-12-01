using Sat.Recruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.UseCasesAbstractions
{
    public interface IUserTypeBonusFactory
    {
        IUserTypeBonus GetBonusCalculator(UserTypeEnum userType);
    }
}
