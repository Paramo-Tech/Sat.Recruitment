using Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);

        Task<User> FindByEmailAsync(string id, CancellationToken cancellationToken);
    }
}
