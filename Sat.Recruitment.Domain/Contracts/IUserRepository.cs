using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAllAsync();
    }
}