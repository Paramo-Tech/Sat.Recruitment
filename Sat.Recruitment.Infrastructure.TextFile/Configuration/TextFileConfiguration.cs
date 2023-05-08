using System.IO;

namespace Sat.Recruitment.Infrastructure.TextFile.Configuration
{
    public class TextFileConfiguration : ITextFileConfiguration
    {
        private const string usersFilePathPartial = "/Files/Users.txt";

        public string TextFilePath()
        {
            return $"{Directory.GetCurrentDirectory()}{usersFilePathPartial}";
        }
    }
}
