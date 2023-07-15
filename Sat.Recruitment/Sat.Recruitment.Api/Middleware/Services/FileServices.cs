using System.Collections.Generic;
using Sat.Recruitment.Api.Application.Request;
using Sat.Recruitment.Api.Middleware.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Middleware.Services
{
    public class FileServices:IFileServices
    {
        public FileServices()
        {

        }
        public async Task<List<UserDTO>> ReadFileUsers(StreamReader reader)
        {
            List<UserDTO>_users=new List<UserDTO>();
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new UserDTO()
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString())
                };
                _users.Add(user);
            }
            reader.Close();
            return _users;
        }

        public StreamReader ReadUsersFromFile()
        {
            var path = $"{Directory.GetCurrentDirectory()}/Files/Users.txt";
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
