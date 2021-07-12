using Domain.Enums;
using Domain.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Factories
{
    public interface IUserMoneyStrategyFactory
    {
        public IUserMoneyStrategy BuildUserMoneyStrategy(UserTypes userType);
    }
}
