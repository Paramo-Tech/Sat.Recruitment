

namespace Sat.Recruitment.Api.Models
{
    public enum UserType
    {
        Normal,
        SuperUser,
        Premium
    }

    public class UserTypeString
    {
        public static UserType GetByName(string name)
        {
            switch (name)
            {
                case "Normal":
                    return UserType.Normal;
                case "SuperUser":
                    return UserType.SuperUser;
                case "Premium":
                    return UserType.Premium;
                default:
                    return UserType.Normal;
            }
        }
    }
}
