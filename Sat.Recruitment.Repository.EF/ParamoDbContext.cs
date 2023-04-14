using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Contracts.Repositories;
using Sat.Recruitment.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Repository.EF
{
    /// <summary>
    /// Entity Framework Context
    /// </summary>
    public class ParamoDbContext : DbContext
    {
        public ParamoDbContext(DbContextOptions<ParamoDbContext> options)
           : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
                
            modelBuilder.Entity<User>()
                .Property(x=> x.Name)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.Email).IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.Address).IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.Phone).IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.UserType);
            modelBuilder.Entity<User>()
                .Property(x => x.Money)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<User>().HasData(new User() { Id=1, Name= "Juan",Email= "Juan @marmol.com",Phone= "+5491154762312", Address="Peru 2464",UserType="Normal",Money=1234 });
            modelBuilder.Entity<User>().HasData(new User() { Id = 2, Name = "Franco", Email = "Franco.Perez@gmail.com", Phone = "+534645213542", Address = "Alvear y Colombres", UserType = "Premium", Money = 112234 });
            modelBuilder.Entity<User>().HasData(new User() { Id = 3, Name = "Agustina", Email = "Agustina@gmail.com", Phone = "+534645213542", Address = "Garay y Otra Calle", UserType = "SuperUser", Money = 112234 });
            
        }
        public new async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }

   
}
