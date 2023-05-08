using Microsoft.Extensions.Logging;
using Sat.Recruitment.Domain.Model;
using Sat.Recruitment.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.SqlServer
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        protected readonly SatRecruitmentDbContext _dbContext;

        public UserRepository(ILogger<UserRepository> logger, SatRecruitmentDbContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public async Task<User> AddAsync(User entity)
        {
            _logger.LogDebug("Adding user to SQLServer DB");
            await _dbContext.Users.AddAsync(entity);
            var result = await _dbContext.SaveChangesAsync();

            if (result != 1)
            {
                _logger.LogError("Failed to add user {userName} to SQLServer DB", entity.Name);
                throw new RepositoryException("An error ocurred while saving your data.");
            }

            _logger.LogDebug("Added user to SQLServer DB");
            return entity;
        }

        public IEnumerable<User> GetAll()
        {
            _logger.LogDebug("Getting all users from SQLServer DB");
            var entities = _dbContext.Users.AsQueryable();
            return entities;
        }      
    }
}
