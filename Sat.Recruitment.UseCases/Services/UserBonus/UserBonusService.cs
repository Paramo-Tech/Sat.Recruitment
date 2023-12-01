using Sat.Recruitment.UseCasesAbstractions;
using Sat.Recruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sat.Recruitment.UseCases.Services.UserBonus
{
    public class UserBonusService : IUserBonusService
    {
        private readonly IUserTypeBonusFactory _userTypeBonusFactory;

        public UserBonusService(IUserTypeBonusFactory userTypeBonusFactory)
        {
            _userTypeBonusFactory = userTypeBonusFactory;
        }

        public decimal CalculateUserBonus(UserTypeEnum userType, decimal money)
        {
            IUserTypeBonus bonusCalculator = _userTypeBonusFactory.GetBonusCalculator(userType);

            return bonusCalculator.CalculateBonus(money);
        }
    }
}
