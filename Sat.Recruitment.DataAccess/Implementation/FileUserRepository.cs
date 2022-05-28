using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Contracts;

namespace Sat.Recruitment.DataAccess.Implementation
{
    public  class FileUserRepository : IUserRepository
    {
        public async Task<IList<UserModel>> GetAllAsync()
        {
            const char fieldSeparator = ',';
            var users = new List<UserModel>();
            using var reader = ReadUsersFromFile();
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                if (line == null) continue;
                var currentUserType = (UserType) Enum.Parse(typeof(UserType), line.Split(',')[4].ToString());
                var fields = line.Split(fieldSeparator);
                var user = new UserModel
                {
                    Name = fields[0],
                    Email = fields[1],
                    Phone = fields[2],
                    Address = fields[3],
                    UserType = currentUserType,
                    Money = decimal.Parse(fields[5]),
                };
                users.Add(user);
            }

            return users;
        }
        
        private StreamReader ReadUsersFromFile()
        {
            const string filesUsersTxt = "/Files/Users.txt";
            var path = $"{Directory.GetCurrentDirectory()}{filesUsersTxt}";

            var fileStream = new FileStream(path, FileMode.Open);

            var reader = new StreamReader(fileStream);
            return reader;
        }
    }
}