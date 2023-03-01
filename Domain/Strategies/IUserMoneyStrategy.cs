using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Strategies
{
    public interface IUserMoneyStrategy
    {
        public decimal CalculateUserMoneyAmount(decimal money);
    }
}
