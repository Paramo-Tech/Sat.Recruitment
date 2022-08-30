using Sat.Recruitment.Api.DTO;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Sat.Recruitment.Api.Repository
{
    public class ReadUsersFromFile:IReadUsersFromFile
    {
        private readonly List<User> _users = new List<User>();
        public ReadUsersFromFile() {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                _users.Add(new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                });
            }
            fileStream.Close();
            reader.Close();
        }

        public bool Exist(UserDto user) {
            if (!_users.Any(x => x.Email == user.Email))
                return true;
            if(!_users.Any(x => x.Phone == user.Phone))
                return true;
            if(!_users.Any(x => x.Name == user.Address && x.Address == user.Address))
                return true;
            return false;
        }
    }
}
