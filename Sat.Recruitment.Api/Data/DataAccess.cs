using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Models;
using System.IO;

namespace Sat.Recruitment.Api.Data
{
    public  static class DataAccess
    {
        public static void ReadUsersFromFile(this IServiceCollection services)
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;

                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };

                DataContext.UserList.Add(user);
            }

            reader.Close();
        }
    }
}
