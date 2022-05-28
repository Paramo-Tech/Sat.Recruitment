using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Sat.Recruitment.DataAccess.Contracts;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Contracts;

namespace Sat.Recruitment.DataAccess.Implementation
{
    public  class StreamUserRepository : IUserRepository
    {
        private readonly IUserTextLineValidator _lineValidator;
        
        //TODO: let see if read a content as StreamReader -> https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string
        private readonly IUsersSourceStream _usersSourceStream;

        public StreamUserRepository(IUsersSourceStream usersSourceStream, IUserTextLineValidator lineValidator )
        {
            _usersSourceStream = usersSourceStream;
            _lineValidator = lineValidator;
        }
        public async Task<IList<User>> GetAllAsync()
        {
            var users = new List<User>();
            using var reader = _usersSourceStream.GetUsers();
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                
                var (fields, lineOk) = _lineValidator.IsValid(line??string.Empty);
                if(!lineOk) continue;
                
                var currentUserType = (UserType) Enum.Parse(typeof(UserType), fields[4]);
                var user = new User
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
        
      
    }
}