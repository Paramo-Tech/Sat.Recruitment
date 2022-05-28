using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Sat.Recruitment.Api.DataAccess.DataObjects;
using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Domain.Contracts;
using Sat.Recruitment.Api.Services.Contracts;

namespace Sat.Recruitment.Api.DataAccess.Implementation
{
    public  class UserRepository : IUserRepository
    {
     

        public async Task<IList<UserModelDto>> GetAllAsync()
        {
            const char fieldSeparator = ',';
            var users = new List<UserModelDto>();
            using var reader = ReadUsersFromFile();
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                if (line == null) continue;
                var currentUserType = (UserType) Enum.Parse(typeof(UserType), line.Split(',')[4].ToString());
                var fields = line.Split(fieldSeparator);
                var user = new UserModelDto
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

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}