using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sat.Recruitment.Domain.Enums
{
    public enum UserTypes
    {
        [Description("Normal")]
        Normal = 0,
        [Description("Premium")]
        Premium = 1,
        [Description("SuperUser")]
        SuperUser = 2
    }
}
