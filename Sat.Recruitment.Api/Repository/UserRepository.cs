using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            var reader = ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result.Split(',');
                var user = new User
                {
                    Name = line[0].ToString(),
                    Email = line[1].ToString(),
                    Phone = line[2].ToString(),
                    Address = line[3].ToString(),
                    UserType = line[4].ToString(),
                    Money = decimal.Parse(line[5].ToString()),
                };
                users.Add(user);
            }

            reader.Close();

            return users;
        }

        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
