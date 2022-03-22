using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Repository.Users
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(UsersContext context) : base(context) { }

        public async Task<User> Authenticate(string email, byte[] password)
        {
            return await _dbSet.Where(x => x.Email == email && x.Password == password && x.IsActive).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllActive(CancellationToken token)
        {
            return await _dbSet.Where(x => x.IsActive).ToListAsync(token);
        }

        public async Task<User> Remove(ulong id, CancellationToken token)
        {
            var user = await GetAsync(id, token);

            if (user == null)
                return null;

            user.IsActive = false;
            await UpdateAsync(user, token);
            return user;
        }
    }
}
