using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Sat.Recruitment.Data.Interfaces;
using Sat.Recruitment.Domain;

namespace Sat.Recruitment.Data
{
    public class UserData: IUserData
    {
        public async  Task<List<User>> GetUsersAsync()
        {
            var users = new List<User>();
            string line;

            using StreamReader readerUser = new StreamReader(new FileStream(Directory.GetCurrentDirectory() + "/Files/Users.txt", FileMode.Open));
            while (readerUser.Peek() >= 0)
            {
                line = await readerUser.ReadLineAsync();
                if (line == null) continue;
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                users.Add(user);
            }

            return users;
        }
    }
}
