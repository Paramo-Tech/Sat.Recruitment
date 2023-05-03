using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public interface ITokenService
    {
        string TokenGen(string idUsuario);
    }
}
