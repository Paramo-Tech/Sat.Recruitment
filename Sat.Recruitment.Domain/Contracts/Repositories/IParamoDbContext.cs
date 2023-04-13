using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Contracts.Repositories
{
    public interface IParamoDbContext
    {
        Task<int> SaveChanges();
    }
}

