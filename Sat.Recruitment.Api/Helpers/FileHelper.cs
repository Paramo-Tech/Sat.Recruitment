using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Helpers
{
    public class FileHelper
    {
        private string _fileName;
        public FileHelper(string fileName)
        {
            _fileName = fileName;
        }

        public List<string> ReadFromFile()
        {
            var lines = new List<string>();
            var path = Directory.GetCurrentDirectory() + string.Format("/Files/{0}.txt", _fileName);
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

    }
}
