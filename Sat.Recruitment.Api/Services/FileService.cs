using System.IO;

namespace Sat.Recruitment.Api.Services
{
    public class FileService
    {
        readonly string path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

        public StreamReader ReadUsersFromFile()
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        public void SaveUserIntoFile(string newUser)
        {
            using StreamWriter writer = File.AppendText(path);
            writer.WriteLine("\n" + newUser);
        }
    }
}
