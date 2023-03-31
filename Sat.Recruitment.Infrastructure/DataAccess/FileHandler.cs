using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Infrastructure.DataAccess
{
    public static class FileHandler
    {
        /// <summary>
        /// Opens the file, read each single line in the file, and then closes the file.
        /// </summary>
        public static void ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);

            FileContext.ListUsers = new List<User>();

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

                FileContext.ListUsers.Add(user);
            }

            reader.Close();
        }

        /// <summary>
        /// Opens the file, appends the user as single string line to the file, and then closes the file.
        /// If the file doesn't exist, is created.
        /// </summary>
        /// <param name="user"></param>
        public static void AppendUserToFile(User user)
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            string value = user.Name + ',' + user.Email + ',' + user.Phone + ',' + user.Address + ',' + user.UserType + ',' +
                           user.Money.ToString().Replace(".", "").Replace(',', '.');

            File.AppendAllText(path, Environment.NewLine + value);
        }
    }
}
