using Sat.Recruitment.Api.Application.Request;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Middleware.Interfaces
{
    public interface IFileServices
    {
        abstract Task<List<UserDTO>> ReadFileUsers(StreamReader reader);
        abstract StreamReader ReadUsersFromFile();
    }
}
