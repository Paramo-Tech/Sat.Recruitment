using Sat.Recruitment.Core.DomainEntities;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.Abstractions.Services
{
    public interface IUserService
    {
        Task<User> Create(User newUser);
    }
}
