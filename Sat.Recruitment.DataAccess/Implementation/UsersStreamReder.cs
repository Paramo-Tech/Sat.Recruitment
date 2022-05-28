using System;
using System.IO;
using Sat.Recruitment.DataAccess.Contracts;

namespace Sat.Recruitment.DataAccess.Implementation
{
    public class UsersFromFile:IUsersSourceStream
    {
        private readonly Func<string> _getFullPath;
        public UsersFromFile(Func<string> getFullPath )
        {
            _getFullPath = getFullPath;
        }
        public StreamReader GetUsers()
        {
            //TODO: let see this https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string
            var fileStream = new FileStream(_getFullPath(), FileMode.Open);
            var reader = new StreamReader(fileStream);
            return reader;
        }
    }
}