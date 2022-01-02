using Sat.Recruitment.Business;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepository : IRepository<User, string>
    {
        IParser<User, string> _userParse;
        List<User> _users;

        public UserRepository(IParser<User, string> userParse)
        {
            _userParse = userParse;
            _users = new List<User>();
            FilePath = "Files/Users.txt";

        }

        public string FilePath { get; set; }

        public Task<bool> Delete(string ID)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Get(string ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAll()
        {
            var users = new List<User>();
            using (var sr = ReadUsersFromFile())
            {
                string line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    User newUser = _userParse.Parse(line);
                    users.Add(newUser);
                }
            }
            return users;

        }

        public async Task<string> Save(User obj)
        {
            _users.Add(obj);
            return _userParse.Unparse(obj);
        }

        private StreamReader ReadUsersFromFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return new StreamReader(fileStream);
        }

    }
}
