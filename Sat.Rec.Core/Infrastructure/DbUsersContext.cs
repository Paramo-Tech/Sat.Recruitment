using Microsoft.EntityFrameworkCore;
using Sat.Rec.Models;

namespace Sat.Rec.Core.Infrastructure
{
    public class DbUsersContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DbUsersContext(DbContextOptions<DbUsersContext> contextOptions) : base(contextOptions)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<GIFUserType> GIFUserTypes { get; set; }
    }
}
