using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Api.Services
{
    public class FileHandlerService : IFileHandlerService
    {
        private StreamReader ReadTxtFile(string file)
        {
            FileStream fileStream = new FileStream(file, FileMode.Open);

            return new StreamReader(fileStream);
        }

        public List<string> GetTxtFileLines(string file)
        {
            var fileLines = new List<string>();

            using(var reader = ReadTxtFile(file))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    fileLines.Add(line);
                }

                return fileLines;
            }

          
        }

    }
}
