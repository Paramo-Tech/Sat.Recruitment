using Users.Domain;

namespace Users.infrastructure.Persistence
{
    public static class UserFileMapper
    {
        public static User ToUser(string line)
        {
            var userProperties = line.Split(',');

            return new User ()
            {
                Name = userProperties[0].ToString(),
                Email = new(userProperties[1].ToString()),
                Phone = new(userProperties[2].ToString()),
                Address = userProperties[3].ToString(),
                UserType = UserType.FromValue(userProperties[4].ToString()),
                Money = decimal.Parse(userProperties[5].ToString()),
            };
        }


        public static string ToLine(User user) => 
            $"{user.Name},{user.Email.Value},{user.Phone.Value},{user.Address},{user.UserType.Value},{user.Money}";
    }
}
