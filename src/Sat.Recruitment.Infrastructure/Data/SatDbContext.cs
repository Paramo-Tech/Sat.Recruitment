using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Entities.UserAggregate;

namespace Sat.Recruitment.Infrastructure.Data
{
	public class SatDbContext : DbContext
	{
		public SatDbContext(DbContextOptions<SatDbContext> options): base(options)
		{            
        }

        public SatDbContext()
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
            builder.Entity<User>().HasData(new User("Juan", "Juan@marmol.com", "+5491154762312", "Peru 2464", "Normal", 1234),
                    new User("Franco", "Franco.Perez@gmail.com", "+534645213542", "Alvear y Colombres", "Premium", 112234),
                    new User("Agustina", "Agustina@gmail.com", "+534641213542", "Garay y Otra Calle", "SuperUser", 112234)
                );
        }
    }
}

