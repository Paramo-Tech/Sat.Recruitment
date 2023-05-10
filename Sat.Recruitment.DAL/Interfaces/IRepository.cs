using Sat.Recruitment.DAL.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.DAL.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T entry);
        void Delete(T entry);
        Task<IEnumerable<T>> Get();
        Task<bool> Find(User entry);
    }
}
