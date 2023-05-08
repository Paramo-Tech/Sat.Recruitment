using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Model;
using Sat.Recruitment.Infrastructure.SqlServer.EntityTypeConfigurations;

namespace Sat.Recruitment.Infrastructure.SqlServer
{
    public class SatRecruitmentDbContext : DbContext
    {
        public SatRecruitmentDbContext(DbContextOptions<SatRecruitmentDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserEntityTypeConfiguration()).HasDefaultSchema("Users");
        }
    }
}
