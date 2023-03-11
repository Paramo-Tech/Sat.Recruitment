using Sat.Recruitment.Services.Interfaces;
using System;
using System.IO;

namespace Sat.Recruitment.Services
{
    public class UsersService : IUsersService
    {
        public UsersService() { }

        public StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
