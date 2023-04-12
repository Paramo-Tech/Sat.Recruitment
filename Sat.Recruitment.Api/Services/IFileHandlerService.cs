using System.Collections.Generic;
using System.IO;

namespace Sat.Recruitment.Api.Services
{
    public interface IFileHandlerService
    {
        List<string> GetTxtFileLines(string file);
    }
}
