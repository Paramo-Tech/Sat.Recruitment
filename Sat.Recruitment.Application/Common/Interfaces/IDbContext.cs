using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Common.Interfaces
{
    public interface IDbContext
    {
        DatabaseFacade Database { get; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
