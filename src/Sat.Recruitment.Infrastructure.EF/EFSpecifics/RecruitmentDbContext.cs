using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Core.DomainEntities;

namespace Sat.Recruitment.Infrastructure.EF.EFSpecifics
{
    public class RecruitmentDbContext : DbContext
    {
        public RecruitmentDbContext(DbContextOptions<RecruitmentDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
    }
}
