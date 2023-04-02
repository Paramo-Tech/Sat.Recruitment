using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<List<User>> GetAllAsync();        
    }
}
