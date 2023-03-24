using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Método que obtiene una entidad por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);
        /// <summary>
        /// Método que obtiene el Json de una entidad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<string> GetByIdJson(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Entry(entity).State = EntityState.Detached;
            return JsonConvert.SerializeObject(entity);
        }
        /// <summary>
        /// Método para obtner por paginas los resutlados de una entidad
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize) => await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        /// <summary>
        /// Método para agregar un elemento a una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        /// <summary>
        /// Método para actualizar una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        /// <summary>
        /// Método para eliminar una entidad sin retornar
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Método para eliminar una entidad retornando null
        /// </summary>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public async Task<T> DeleteAsync(int entityID)
        {
            if (await _dbContext.Set<T>().FindAsync(entityID) is T entity)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            return null;
        }
        /// <summary>
        /// Método que obtiene todos los registro de una entidad
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext
                 .Set<T>()
                 .ToListAsync();
        /// <summary>
        /// Método que consulta una entidad por medio expresión construida multiples registros
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetFilterAsync(Expression<Func<T, bool>> predicate) => await _dbContext.Set<T>().Where(predicate).ToListAsync();
        /// <summary>
        /// Método que consulta una entidad por medio expresión construida unico registro
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> GetFilterFirstOrDefaultAsync(Expression<Func<T, bool>> predicate) => await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
        /// <summary>
        /// Método para agregar multiples resitros a una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity)
        {
            try
            {
                await _dbContext.AddRangeAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
            return entity;
        }
        /// <summary>
        /// Método para actualizar multiples resitros a una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entity)
        {
            foreach (var item in entity)
            {
                _dbContext.Entry<T>(item).State = EntityState.Detached;
            }
            _dbContext.UpdateRange(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

    }
}
