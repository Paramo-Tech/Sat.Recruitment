using System;
using System.IO;

namespace Sat.Recruitment.Api.Services
{
    public class FileService
    {
        public StreamReader ReadUsersFromFile()
        {
            string path = FindSolutionDirectory() + "\\Sat.Recruitment.Api\\Files\\Users.txt";
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        public void SaveUserIntoFile(string newUser)
        {
            string path = FindSolutionDirectory() + "\\Sat.Recruitment.Api\\Files\\Users.txt";
            using StreamWriter writer = File.AppendText(path);
            writer.WriteLine(newUser);
            writer.Flush();
            writer.Close();
        }

        public void ClearFile()
        {
            string path = FindSolutionDirectory() + "\\Sat.Recruitment.Api\\Files\\Users.txt";
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(string.Empty);
            }
        }

        private string FindSolutionDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);

            while (directoryInfo != null)
            {
                FileInfo[] solutionFiles = directoryInfo.GetFiles("*.sln");
                if (solutionFiles.Length > 0)
                {
                    return directoryInfo.FullName;
                }

                directoryInfo = directoryInfo.Parent;
            }

            return null; 
        }
    }
}
