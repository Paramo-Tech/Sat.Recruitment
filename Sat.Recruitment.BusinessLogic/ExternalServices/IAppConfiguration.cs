using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.BusinessLogic.ExternalServices
{
    public interface IAppConfiguration
    {
        string FilePath { get; set; }
    }
}
