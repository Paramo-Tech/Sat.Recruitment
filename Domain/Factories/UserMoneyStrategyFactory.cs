using Domain.Enums;
using Domain.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Factories
{
    public class UserMoneyStrategyFactory : IUserMoneyStrategyFactory
    {
        public IUserMoneyStrategy BuildUserMoneyStrategy(UserTypes userType)
        {
            IUserMoneyStrategy strategy;
            switch (userType)
            {
                case UserTypes.Normal:
                    strategy = new NormalUserMoneyStrategy();
                    break;
                case UserTypes.SuperUser:
                    strategy = new SuperUserMoneyStrategy();
                    break;
                case UserTypes.Premium:
                    strategy = new PremiumUserMoneyStrategy();
                    break;
                default: 
                    strategy = new NormalUserMoneyStrategy();
                    break;
            }

            return strategy;
        }
    }
}
