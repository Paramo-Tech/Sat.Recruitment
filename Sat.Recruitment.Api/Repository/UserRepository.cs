using Sat.Recruitment.Api.Entitys;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repository
{
    public class UserRepository
    {
        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        public async Task<IEnumerable<User>> LoadUsersAsync()
        {
            var users = new List<User>();
            using (var readerUser = ReadUsersFromFile())
            {
                string line;
                while ((line = await readerUser.ReadLineAsync()) != null)
                {
                    var lineSeparated = line.Split(',');
                    var userToAdd = new User()
                    {
                        Name = lineSeparated[0],
                        Email = lineSeparated[1],
                        Phone = lineSeparated[2],
                        Address = lineSeparated[3],
                        UserType = lineSeparated[4],
                        Money = decimal.Parse(lineSeparated[5]),
                    };
                    users.Add(userToAdd);
                }
                readerUser.Close();
            }
            return users;
        }
    }
}
