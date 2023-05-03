using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public interface IGetPrivileges
    {
        string GetPrivilegesToUser(int id);
    }
}
