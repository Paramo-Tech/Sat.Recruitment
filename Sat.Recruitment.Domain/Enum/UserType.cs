using System.Runtime.Serialization;

namespace Sat.Recruitment.Domain.Enum
{
    public enum UserType
    {
        [EnumMember(Value = "Normal")]
        Normal,

        [EnumMember(Value = "SuperUser")]
        SuperUser,

        [EnumMember(Value = "Premium")]
        Premium
    }
}
