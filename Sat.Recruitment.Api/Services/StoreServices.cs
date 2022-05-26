using System.IO;
using Sat.Recruitment.Api.Services.Contracts;

namespace Sat.Recruitment.Api.Services
{
    public  class StoreServices : IStoreServices
    {
        public StreamReader ReadUsersFromFile()
        {
            const string filesUsersTxt = "/Files/Users.txt";
            var path = $"{Directory.GetCurrentDirectory()}{filesUsersTxt}";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}