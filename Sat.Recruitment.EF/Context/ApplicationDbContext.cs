using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.DTOs.Models;

namespace Sat.Recruitment.EF.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
