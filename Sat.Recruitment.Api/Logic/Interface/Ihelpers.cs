using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Logic.Interface
{
    interface Ihelpers
    {
        public string normalizeEmail(string email);
        public decimal convertMoney(decimal money, decimal percentage);
    }
}
