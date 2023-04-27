using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Infrastructure.Persistence
{
    public class SatDbContext : DbContext, IDbContext
    {
        public SatDbContext()
        {
        }

        public SatDbContext(DbContextOptions<SatDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });
        }
    }
}
