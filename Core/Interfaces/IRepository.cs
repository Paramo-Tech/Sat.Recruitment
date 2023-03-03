using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// IRepository Interface.
    /// It should be more methods like Add, Delete etc
    /// </summary>
    public interface IRepository<T>
    where T : class
    {
        public Task<List<T>> GetAll();
        public Task Add(T entity);
    }
}
