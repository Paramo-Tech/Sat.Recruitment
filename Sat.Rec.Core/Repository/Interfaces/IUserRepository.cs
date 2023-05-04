using Sat.Rec.Models;

namespace Sat.Rec.Core.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByAddress(string address);
        Task<User?> GetByEmail(string email);
        Task<User?> GetByName(string name);
        Task<User?> GetByPhone(string phone);
    }
}
