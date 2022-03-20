using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base (options) { }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<User>()
              .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
               .Property(x => x.Name)
               .HasMaxLength(50)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(x => x.Password)
               .IsRequired();

            modelBuilder.Entity<User>()
              .Property(x => x.Email)
              .HasMaxLength(25)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(x => x.Address)
              .HasMaxLength(60)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(x => x.Phone)
              .HasMaxLength(15)
              .IsRequired();            

            modelBuilder.Entity<User>()
              .Property(x => x.UserType)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(x => x.Address)
              .HasMaxLength(60)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(x => x.Money)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(x => x.IsActive)
              .IsRequired();

            modelBuilder.Entity<User>()
              .HasAlternateKey(x => x.Email);

            modelBuilder.Entity<User>()
              .HasAlternateKey(x => x.Phone);

            modelBuilder.Entity<User>()
                .HasIndex(x => new { x.Name, x.Address })
                .IsUnique();
                
            #endregion
        }
    }
}
