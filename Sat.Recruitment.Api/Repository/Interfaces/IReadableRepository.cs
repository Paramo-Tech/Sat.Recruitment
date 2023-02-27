using Sat.Recruitment.Api.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Repository.Interfaces
{
    public interface IReadableRepository<T> where T:IIdentifiable
    {
        T Get(Func<T, bool> func);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Func<T, bool> func);
        bool Any(Func<T, bool> func);
    }
}
