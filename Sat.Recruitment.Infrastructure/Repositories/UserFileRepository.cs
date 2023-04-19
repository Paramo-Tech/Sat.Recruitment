using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sat.Recruitment.Infrastructure.Repositories
{
    public class UserFileRepository : IUserRepository
    {
        private readonly string _filePath;

        public UserFileRepository(string filePath)
        {
            _filePath = filePath;
        }

        public bool AddUser(USER user)
        {
            try
            {
                using (var fileStream = new FileStream(_filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    string userLine = $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}";
                    streamWriter.WriteLineAsync(userLine);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding the u: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<USER> GetAllUsers()
        {
            return GetUsers();
        }

        public bool IsUserDuplicated(USER user)
        {

            var users = GetAllUsers();

            return users.Any(u =>
                (u.Email == user.Email || u.Phone == user.Phone) ||
                (u.Name == user.Name && u.Address == user.Address));
        }

        private List<USER> GetUsers()
        {
            List<USER> _userList = new List<USER>();

            try
            {
                var reader = ReadUsersFromFile();

                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLineAsync().Result;
                    var UserDto = new USER
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = line.Split(',')[4].ToString(),
                        Money = decimal.Parse(line.Split(',')[5].ToString()),
                    };
                    _userList.Add(UserDto);
                }
                reader.Close();

                return _userList;
            }
            catch (Exception)
            {
                return _userList;
            }
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
