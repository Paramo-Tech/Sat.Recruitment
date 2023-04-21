using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Models;
using System;

namespace Sat.Recruitment.Api.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserTypeModel> UsersTypes { get; set; }


    }
}
