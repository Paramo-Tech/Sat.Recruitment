using Sat.Recruitment.Api.Models;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.Interfaces
{
    public interface IReadUserFile
    {
        StreamReader ReadUsersFromFile(string directory);

        bool WriteFileUsers(User user, string directory);

    }
}
