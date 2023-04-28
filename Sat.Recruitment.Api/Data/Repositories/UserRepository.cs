using Microsoft.OpenApi.Extensions;
using Sat.Recruitment.Api.Business.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private List<User> _userList;

        private readonly string _filePath;

        public UserRepository()
        {
            _filePath = Directory.GetCurrentDirectory() + "/Files/Users.txt";
        }

        public async Task<bool> Create(User entity)
        {
            await GetAllAsync();

            if (IsDuplicated(entity))
            {
                return false;
            }
            else
            {
                _userList.Add(entity);
                await InsertNewUserAsync(entity);
            }

            return true;
        }

        private async Task InsertNewUserAsync(User user)
        {
            using (StreamWriter outputFile = new StreamWriter(_filePath, true))
            {
                string line = $"\n{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType.GetDisplayName()},{user.Money}";
                await outputFile.WriteAsync(line);
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            _userList = new List<User>();

            using (StreamReader reader = ReadUsersFromFile())
            {
                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
                    string[] parts = line.Split(',');

                    bool success = decimal.TryParse(parts[5], out decimal value);

                    var user = new User
                    {
                        Name = parts[0],
                        Email = parts[1],
                        Phone = parts[2],
                        Address = parts[3],
                        UserType = Enum.Parse<UserType>(parts[4]),
                        Money = success ? value : 0
                    };

                    _userList.Add(user);
                }
            }
            return _userList;
        }

        private bool IsDuplicated(User user)
        {
            return _userList.Any(x =>
                x.Email == user.Email || x.Phone == user.Phone ||
                x.Name == user.Name && x.Address == user.Address);
        }

        private StreamReader ReadUsersFromFile()
        {
            FileStream fileStream = new FileStream(_filePath, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);

            return reader;
        }
    }
}
