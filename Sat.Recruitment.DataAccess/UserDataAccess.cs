using Sat.Recruitment.Infrastructure.Interfaces.DataAccess;
using Sat.Recruitment.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sat.Recruitment.DataAccess
{
    public class UserDataAccess:EntityBase<User>, IUserDataAccess
    {
        private readonly ICollection<User> _users=new List<User>();

        public UserDataAccess()
        {
            LoadUsers();
        }

        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
        protected virtual void LoadUsers()
        {
            var reader = ReadUsersFromFile();

            //Normalize email

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
                    OriginalMoney = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(user);
            }
            reader.Close();
        }

    }
}
