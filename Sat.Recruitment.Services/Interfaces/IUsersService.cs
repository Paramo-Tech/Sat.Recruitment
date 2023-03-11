using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sat.Recruitment.Services.Interfaces
{
    public interface IUsersService
    {
        StreamReader ReadUsersFromFile();
    }
}
