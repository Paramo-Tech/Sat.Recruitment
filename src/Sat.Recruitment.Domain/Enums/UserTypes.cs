using System.ComponentModel;

namespace Sat.Recruitment.Domain.Enums
{
    public enum UserTypes
    {
        [Description(nameof(None))]
        None = 0,
        [Description(nameof(Normal))]
        Normal = 1,
        [Description(nameof(SuperUser))]
        SuperUser = 2,
        [Description(nameof(Premium))]
        Premium = 3
    }
}
