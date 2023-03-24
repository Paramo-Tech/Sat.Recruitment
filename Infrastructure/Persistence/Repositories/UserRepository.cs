using Domain.Entities;
using Domain.Interfaces.Repository;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository : GenericRepositoryAsync<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        public async Task<User> AddUser(User user) => await AddAsync(user);
        public async Task<IEnumerable<User>> GetAllUser() => await GetAllAsync();
        public async Task<User> UpdateUser(User user) => await UpdateAsync(user);

    }
  

}
