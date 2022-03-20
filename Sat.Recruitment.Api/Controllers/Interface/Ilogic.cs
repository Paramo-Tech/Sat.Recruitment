using Sat.Recruitment.Api.Controllers.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers.Interface
{
    public interface Ilogic
    {
        Result CreateUser(RequestUser request);
    }
}
