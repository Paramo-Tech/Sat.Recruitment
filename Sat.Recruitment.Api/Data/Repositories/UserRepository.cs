using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Context;
using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<User> Add(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public IEnumerable<User> Get()
        {
            var users = _dbContext.Users.AsEnumerable();
            return users;
        }
    }
}
