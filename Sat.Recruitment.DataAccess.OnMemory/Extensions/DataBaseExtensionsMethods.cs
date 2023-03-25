using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.DataAccess.OnMemory.DataBase;
using Sat.Recruitment.Domain.Models.Enum;
using Sat.Recruitment.Domain.Models.Users;

namespace Sat.Recruitment.DataAccess.OnMemory.Extensions
{
    public static class DataBaseExtensionsMethods
    {
        public static void LoadOnMemoryDataBase(this IServiceCollection services)
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
                    UserType = Enum.Parse<EUserType>(line.Split(',')[4].ToString()),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                OnMemoryDataBase.Users.Add(user);
            }

            reader.Close();
        }
    }
}
