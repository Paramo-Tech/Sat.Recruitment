using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IApplicationDbContext
    {
        public IDbConnection Connection {get;}
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellation);
    }
}
