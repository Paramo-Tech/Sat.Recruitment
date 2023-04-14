using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Contracts.Repositories;
using Sat.Recruitment.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Repository.EF
{
    public class UserRepository : IUserRepository
    {
        private ParamoDbContext _paramoDbContext;
        public UserRepository(ParamoDbContext dbContext)
        {
            _paramoDbContext = dbContext;
        }
        public async Task AddUser(User user)
        {
            await _paramoDbContext.Users.AddAsync(user);
            await _paramoDbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsers()
        {
           return  await _paramoDbContext.Users.ToListAsync();
        }
    }
}
