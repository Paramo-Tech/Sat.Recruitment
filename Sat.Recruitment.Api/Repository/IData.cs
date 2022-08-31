using Sat.Recruitment.Api.DTO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repository
{
    public interface IData
    {
        bool Exist(UserDto user);
        Task Save(UserDto user);
    }
}
