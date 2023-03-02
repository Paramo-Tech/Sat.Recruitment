using Sat.Recruitment.Api.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repository.Interfaces
{
    public interface IGenericRepository<T> : IReadableRepository<T>, IWritableRepository<T>
        where T : IIdentifiable 
    {

    }
}
