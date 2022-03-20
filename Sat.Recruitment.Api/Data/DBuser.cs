using Sat.Recruitment.Api.Logic;
using Sat.Recruitment.Api.Logic.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Data
{
    public class DBuser : IbdUser
    {
        public List<User> getUsers()
        {
            try{ 
            string path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            List<User> users = new List<User>();
            while (reader.Peek() >= 0)
            {
                string line = reader.ReadLineAsync().Result;
                User user = new User
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
            reader.Close();
            
            return users;
            }
            catch 
            {
                throw new InvalidOperationException("Connection error");
            }
        }
    }
}
