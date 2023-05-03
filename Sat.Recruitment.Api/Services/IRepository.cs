using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
        T GetById(int id);
        IEnumerable<T> GetAll();
        bool Exists(T entity);
    }
}
