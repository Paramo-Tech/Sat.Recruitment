using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Entities.UserAggregate;

namespace Sat.Recruitment.Infrastructure.Data
{
    public class SatDbContext : DbContext
    {
        public SatDbContext(DbContextOptions<SatDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            SeedData(builder);
        }

        private static void SeedData(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(new User("Juan", "Juan@marmol.com", "Peru 2464", "+5491154762312", "Normal", 1234, 1),
                    new User("Franco", "Franco.Perez@gmail.com", "Alvear y Colombres", "+534645213542", "Premium", 112234, 2),
                    new User("Agustina", "Agustina@gmail.com", "Garay y Otra Calle", "+534641213542", "SuperUser", 112234, 3)
                );
        }
    }
}

