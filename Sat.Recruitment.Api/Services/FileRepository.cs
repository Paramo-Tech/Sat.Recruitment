using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Api.Services
{
    internal class FileRepository
    {
        internal static string GetFilePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Files/Users.txt");
        }
        internal static StreamReader ReadUsersFromFile()
        {
            var path = GetFilePath();

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        internal async static void AppendLines(IEnumerable<string> lines)
        {
            await File.AppendAllLinesAsync(GetFilePath(), lines);
        }
    }
}
