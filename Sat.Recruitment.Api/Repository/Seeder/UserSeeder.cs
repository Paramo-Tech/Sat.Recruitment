using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.DTO;
using Sat.Recruitment.Api.Models.Interfaces;
using Sat.Recruitment.Api.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Api.Repository.Seeder
{
    public class UserSeeder : IUserSeeder
    {
        private readonly IUserRepository _Repository;
        private readonly IUserFactory _factory;

        public UserSeeder(IUserRepository repository, IUserFactory factory)
        {
            _Repository = repository;
            _factory = factory;
        }

        public void SeedUsers()
        {

            var reader = ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var datos = line.Split(',');
                var newUserDto = new UserDTO
                {
                    Name = datos[0].ToString(),
                    Email = datos[1].ToString(),
                    Phone = datos[2].ToString(),
                    Address = datos[3].ToString(),
                    UserType = (UserTypes)Enum.Parse(typeof(UserTypes), datos[4].ToString(), true),
                    Money = decimal.Parse(datos[5].ToString()),
                };
                var newUser=_factory.CreateUser(newUserDto);
                _Repository.Add(newUser);
            }

            reader.Close();
        }

        private static StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
