using System;
using System.Collections.Generic;
using System.Text;
using Sat.Recruitment.Domain.Domains;

namespace Sat.Recruitment.Services.Definitions
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        User Handle(User user);
    }
}
