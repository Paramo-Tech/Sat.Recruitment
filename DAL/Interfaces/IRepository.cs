using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.DAL.Interfaces
{
    /// <summary>
    /// Interfaz to implement the Data access layer for each entity
    /// </summary>
    /// <typeparam name="T">Business Object</typeparam>
    /// <typeparam name="K">ID</typeparam>
    public interface IRepository<T,K> where T : class
    {
        Task<List<T> >GetAll();
         Task<K> Save(T businessObject);

        Task<T> Get(K ID);

        Task<bool> Delete(K ID);
    }
}
