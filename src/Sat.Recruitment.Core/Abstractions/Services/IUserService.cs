using Sat.Recruitment.Core.DomainEntities;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.Abstractions.Services
{
    public interface IUserService
    {
        User Create(User newUser);
    }
}
