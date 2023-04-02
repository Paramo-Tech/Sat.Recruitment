using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace Infrastructure.Persistence
{
    public static class DataAccess
    {
        public static List<User> GetUsers()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            var _users = new List<User>();


            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(user);
            }
            reader.Close();

            return _users;
        }

        public static void AppendUser(User user)
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            string value = user.Name + ',' + user.Email + ',' + user.Phone + ',' + user.Address + ',' +
                           user.UserType + ',' + user.Money.ToString().Replace(".", "").Replace(',', '.');

            File.AppendAllText(path, Environment.NewLine + value);
        }
    }
}