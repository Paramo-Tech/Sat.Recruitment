using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;
        public UserRepository(StreamReader streamReader, StreamWriter streamWriter) {
            _reader = streamReader;
            _writer = streamWriter;
        }
        public async Task<User> Insert(User user)
        {
            try
            {
                if (await IsDuplicated(user))
                {
                    throw new DuplicateNameException("User duplicated");
                }
                string newLine = $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}";
                await _writer.WriteLineAsync(newLine);
                return user;
            }
            catch(DuplicateNameException ex)
            {
                throw new DuplicateNameException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> IsDuplicated(User user)
        {
            try
            {
                string usersFile = await _reader.ReadToEndAsync();
                List<User> users = usersFile
                    .Split("\r\n")
                    .Where(x => x != null && x != string.Empty)
                    .Select(line => new User
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = line.Split(',')[4].ToString(),
                        Money = double.Parse(line.Split(',')[5].ToString())
                    }).ToList();
                return users.Any(u => (u.Name == user.Name && u.Address == user.Address) || u.Email == user.Email || u.Phone == user.Phone);
            }
            catch(Exception ex) { 
                throw new Exception(ex.Message, ex); 
            }
        }
    }
}
