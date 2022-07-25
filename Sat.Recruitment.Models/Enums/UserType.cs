using System.ComponentModel;

namespace Sat.Recruitment.Models.Enums
{
    public enum UserType
    {
        [Description("Normal")]
        Normal,

        [Description("Premium")]
        Premium,

        [Description("Super user")]
        SuperUser
    }
}