using Microsoft.Extensions.Options;
using Sat.Recruitment.Api.ViewModels;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Respositories;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Infrastructure.Data
{
    public class FileRepository: IUserRepository
    {
        private readonly FilePathsConfiguration _filePathsConfiguration;

        public FileRepository(IOptions<FilePathsConfiguration> filePathsConfiguration)
        {
            _filePathsConfiguration = filePathsConfiguration.Value;
        }
        public void CreateUser(User user)
        {
            var users = ReadUsers();
            users.Add(user);
            WriteUsers(users);
        }

        public User GetByEmail(string email)
        {
            var users = ReadUsers();
            return users.Find(u => u.Email == email);
        }

        public List<User> ReadUsers()
        {
            var users = new List<User>();

            FileStream fileStream = new FileStream(_filePathsConfiguration.UsersTextFile, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var userToAdd = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = Enum.Parse<UserType>(line.Split(',')[4].ToString()),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                users.Add(userToAdd);
            }
            reader.Close();


            return users;
        }

        private void WriteUsers(List<User> users)
        {
            FileStream fileStream = new FileStream(_filePathsConfiguration.UsersTextFile, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                foreach (var user in users)
                {
                    writer.WriteLine($"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}");
                }
            }
        }
    }
}
