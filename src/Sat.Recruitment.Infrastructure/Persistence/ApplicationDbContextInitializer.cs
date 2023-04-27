using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Enums;

namespace Sat.Recruitment.Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _dbContext;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_dbContext.Database.IsSqlServer())
                {
                    await _dbContext.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!_dbContext.Users.Any())
            {
                _dbContext.Users.AddRange(new List<User>
                {
                    new User(name: "Juan", email: "Juan@marmol.com", phone: "+5491154762312", address: "Peru 2464", userType: UserTypes.Normal, money: 1234M, isNewUser: true),
                    new User(name: "Franco", email: "Franco.Perez@gmail.com", phone: "+534645213542", address: "Alvear y Colombres", userType: UserTypes.Premium, money: 112234M, isNewUser: true),
                    new User(name: "Agustina", email: "Agustina@gmail.com", phone: "+534645213542", address: "Garay y Otra Calle", userType: UserTypes.SuperUser, money: 112234M, isNewUser : true)
                });

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
