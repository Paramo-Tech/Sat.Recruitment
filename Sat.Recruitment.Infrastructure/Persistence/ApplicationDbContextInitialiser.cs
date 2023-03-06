using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
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
            if (!_context.Users.Any())
            {
                await _context.Users.AddAsync(new User
                {
                    Name = "Juan",
                    Email = "Juan@marmol.com",
                    Phone = "+5491154762312",
                    Address = "Peru 2464",
                    UserType = "Normal",
                    Money = 1234
                });
                await _context.Users.AddAsync(new User
                {
                    Name = "Franco",
                    Email = "Franco.Perez@gmail.com",
                    Phone = "+534645213542",
                    Address = "Alvear y Colombres",
                    UserType = "Premium",
                    Money = 112234
                });
                await _context.Users.AddAsync(new User
                {
                    Name = "Agustina",
                    Email = "Agustina@gmail.com",
                    Phone = "+534641213542",
                    Address = "Garay y Otra Calle",
                    UserType = "SuperUser",
                    Money = 112234
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
