using Sat.Recruitment.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sat.Recruitment.DAccess.Contracts
{
    public interface IUserDAccess
    {
        User MapFromLineToObject(string line);

        string MapFromObjectToLine(User item);

    }

}
