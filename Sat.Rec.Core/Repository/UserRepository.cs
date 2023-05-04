using Microsoft.EntityFrameworkCore;
using Sat.Rec.Core.Infrastructure;
using Sat.Rec.Core.Repository.Interfaces;
using Sat.Rec.Models;

namespace Sat.Rec.Core.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbUsersContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetByAddress(string address)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Address == address);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetByName(string name)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<User?> GetByPhone(string phone)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Phone == phone);
        }
    }
}
