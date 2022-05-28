using System.IO;

namespace Sat.Recruitment.DataAccess.Contracts
{
    public interface IUsersSourceStream
    {
        StreamReader GetUsers();
    }
}