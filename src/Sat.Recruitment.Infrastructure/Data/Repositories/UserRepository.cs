using System;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Domain.Entities.UserAggregate;
using Sat.Recruitment.Domain.Interfaces.Data.Repositories;

namespace Sat.Recruitment.Infrastructure.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly SatDbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;        

        public UserRepository(SatDbContext dbContext, ILogger<UserRepository> logger)
		{
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> AddUserAsync(User user)
        {
            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                _logger.LogDebug("User saved");

                return user.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("User: " + user.ToString() + "not saved, error in db + " + ex.Message);
                return -1;
            }            
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<bool> IsDublicateUser(string name, string email, string address, string phone)
        {
            if (_dbContext.Users
                .Where(x=>
                    (x.Email == email || x.Phone == phone) ||
                    (x.Name == name && x.Address == address)).Any())
            {
                return true;
            }

            return false;
        }
    }
}

