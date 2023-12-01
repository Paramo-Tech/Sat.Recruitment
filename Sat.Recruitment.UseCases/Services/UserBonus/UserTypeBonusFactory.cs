using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Entities;
using Sat.Recruitment.UseCasesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.UseCases.Services.UserBonus
{
    public class UserTypeBonusFactory : IUserTypeBonusFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public UserTypeBonusFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUserTypeBonus GetBonusCalculator(UserTypeEnum userType)
        {
            switch (userType)
            {
                case UserTypeEnum.Normal:
                    return _serviceProvider.GetService<NormalUserType>()!;
                case UserTypeEnum.SuperUser:
                    return _serviceProvider.GetService<SuperUserType>()!;
                case UserTypeEnum.Premium:
                    return _serviceProvider.GetService<PremiumUserType>()!;
                default:
                    throw new NotSupportedException($"UserType '{userType}' is not supported.");
            }
        }
    }
}
