using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.Service
{
    public class ReadUserFileService : IReadUserFile
    {
        public StreamReader ReadUsersFromFile(string directory)
        {
            var path = Directory.GetCurrentDirectory() + directory;

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        public bool WriteFileUsers(User user, string directory)
        {
            var path = Directory.GetCurrentDirectory() + directory;
            try
            {
                var money = user.Money.ToString().Replace(",", ".");
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    string line = $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{money}";
                    writer.WriteLine(line);
                    writer.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
                throw;
            }
           
        }
    }
}
