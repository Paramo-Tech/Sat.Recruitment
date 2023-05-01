using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Entities;
using System.Reflection;
using Sat.Recruitment.Application.Common.Interfaces;

namespace Sat.Recruitment.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext , IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
            
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}