using Sat.Recruitment.Api.Entitys;
using Sat.Recruitment.Api.Integration;
using System.Threading.Tasks;


namespace Sat.Recruitment.Api.Services
{
    public interface IUserBussiness
    {
        Task<Result> AddUser(User user);
    }
}
