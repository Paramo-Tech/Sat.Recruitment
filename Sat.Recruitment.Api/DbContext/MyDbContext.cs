using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;

namespace Sat.Recruitment.Api.DbContext
{
    public class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
        {
        }
        
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .HasDiscriminator<UserType>("UserType")
            .HasValue<Premium>(UserType.Premium)
            .HasValue<Normal>(UserType.Normal)
            .HasValue<SuperUser>(UserType.SuperUser);

            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.ToTable("users");

            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.Id)
            //        .HasColumnName("id")
            //        .ValueGeneratedOnAdd();

            //    entity.Property(e => e.Name)
            //        .HasColumnName("name")
            //        .HasMaxLength(100)
            //        .IsRequired();

            //    entity.Property(e => e.Email)
            //        .HasColumnName("email")
            //        .HasMaxLength(100)
            //        .IsRequired();

            //    entity.Property(e => e.Address)
            //        .HasColumnName("address")
            //        .HasMaxLength(200)
            //        .IsRequired();

            //    entity.Property(e => e.Phone)
            //        .HasColumnName("phone")
            //        .HasMaxLength(20)
            //        .IsRequired();

            //    entity.Property(e => e.UserType)
            //        .HasColumnName("usertype")
            //        .HasMaxLength(20)
            //        .IsRequired();

            //    entity.Property(u => u.Money)
            //        .HasColumnName("money")
            //        .HasColumnType("decimal(18,2)")
            //        .IsRequired();
            //});
        }

    }
}
