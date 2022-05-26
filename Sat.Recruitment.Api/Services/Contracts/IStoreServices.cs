using System.IO;

namespace Sat.Recruitment.Api.Services.Contracts
{
    public interface IStoreServices
    {
        StreamReader ReadUsersFromFile();
    }
}