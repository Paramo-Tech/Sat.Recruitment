using Sat.Recruitment.Application.Contracts.Application;
using Sat.Recruitment.Application.Contracts.Persistence;
using Sat.Recruitment.Application.Helpers;
using Sat.Recruitment.Application.Models;
using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sat.Recruitment.Infrastructure.Persistence
{
    public class UserPersistence : IUserRepository
    {
        public List<User> GetUsers()
        {
            var path = $"{AppDomain.CurrentDomain.BaseDirectory}Files\\Users.txt";
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);

            var listUsers = new List<User>();

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
                listUsers.Add(user);
            }
            reader.Close();
            return listUsers;
        }
    }
}
