﻿using Users.Domain;

namespace Users.infrastructure.Persistence
{
    public static class UserFileMapper
    {
        public static User ToUser(string line) => new()
        {
            Name = line.Split(',')[0].ToString(),
            Email = new(line.Split(',')[1].ToString()),
            Phone = new(line.Split(',')[2].ToString()),
            Address = line.Split(',')[3].ToString(),
            UserType = UserType.FromValue(line.Split(',')[4].ToString()),
            Money = decimal.Parse(line.Split(',')[5].ToString()),
        };

        public static string ToLine(User user) => 
            $"{user.Name},{user.Email.Value},{user.Phone.Value},{user.Address},{user.UserType.Value},{user.Money}";
    }
}
