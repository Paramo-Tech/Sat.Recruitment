using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Common.Interfaces
{
    public  interface IApplicationDbContext
    {
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
