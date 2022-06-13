using Users.Domain;

namespace Users.infrastructure.Persistence
{
    public static class UserFileMapper
    {
        public static User ToUser(string line)
        {
            var userProperties = line.Split(',');

            return new User (
                userProperties[0].ToString(),
                new(userProperties[1].ToString()),
                userProperties[3].ToString(),
                new(userProperties[2].ToString()),
                UserType.FromValue(userProperties[4].ToString()),
                decimal.Parse(userProperties[5].ToString())
            );
        }


        public static string ToLine(User user) => 
            $"{user.Name},{user.Email.Value},{user.Phone.Value},{user.Address},{user.UserType.Value},{user.Money}";
    }
}
