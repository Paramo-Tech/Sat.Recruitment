using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Models;
using System.Reflection.Metadata;

namespace Sat.Recruitment.Api.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=./Files/paramo.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(b => b.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(b => b.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(b => b.Phone).IsUnique();
        }
    }
}
