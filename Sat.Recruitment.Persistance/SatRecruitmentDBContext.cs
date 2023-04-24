using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Persistence.Configurations;

namespace Sat.Recruitment.Persistance
{
    public class SatRecruitmentDBContext: DbContext
    {
        public SatRecruitmentDBContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserConfiguration().Configure(modelBuilder.Entity<UserE>());
        }
       
        public DbSet<UserE> UsersDB { get; set; }
    }
}
