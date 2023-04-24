using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Persistence.Repositories
{
    public class UserFileRepository : IUserFileRepository
    {
        private readonly IUserFileRepository userFileRepository;
        public UserFileRepository(IUserFileRepository userFileRepository) 
        { 
            this.userFileRepository = userFileRepository;
        }

        private string GetFilePath()
        {
            return Directory.GetCurrentDirectory() + "/Files/Users.txt";
        }

        public StreamReader ReadUsersFromFile()
        {
            var path = GetFilePath();

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        public bool SaveUserInFileAsync(string userLine)
        {
            try
            {
                var fileStream = new FileStream(GetFilePath(), FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                using (StreamWriter writetext = new StreamWriter(fileStream))
                {
                    writetext.WriteLineAsync(userLine);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<UserE>> GetAllUsersAsync()
        {
            var result = new List<UserE>();
            using (var reader = ReadUsersFromFile())
            {
                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
                    var user = new UserE
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = UserTypeString.GetByName(line.Split(',')[4].ToString()),
                        Money = decimal.Parse(line.Split(',')[5].ToString())
                    };
                    result.Add(user);
                }
                reader.Close();

                return result;
            }
        }
    }
}
