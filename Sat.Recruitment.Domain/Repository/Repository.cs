using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly UsersContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(UsersContext context) 
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            SaveChanges();
            return Get(entity.Id);
        }

        public async Task<T> AddAsync(T entity, CancellationToken token)
        {
            await _context.Set<T>().AddAsync(entity, token);
            await SaveChangesAsync(token);
            return await GetAsync(entity.Id, token);
        }

        public void Delete(ulong id)
        {
            var entity = Get(id);
            _context.Set<T>().Remove(entity);
            SaveChanges();
        }

        public async Task DeleteAsync(ulong id, CancellationToken token)
        {
            var entity = await GetAsync(id, token);
            _context.Set<T>().Remove(entity);
            await SaveChangesAsync(token);
        }

        public T Get(ulong id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<List<T>> GetAllAsync(CancellationToken token)
        {
            return await _context.Set<T>().ToListAsync(token);
        }

        public async Task<T> GetAsync(ulong id, CancellationToken token)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            SaveChanges();
            return Get(entity.Id);
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken token)
        {
            _context.Set<T>().Update(entity);
            await SaveChangesAsync(token);
            return await GetAsync(entity.Id, token);
        }

        protected void SaveChanges()
        {
            _context.SaveChanges();
        }

        protected async Task SaveChangesAsync(CancellationToken token)
        {
            await _context.SaveChangesAsync(token);
        }

        public async Task AddRangeAsync(List<T> entities, CancellationToken token)
        {
            _context.AddRange(entities);
            await _context.SaveChangesAsync(token);
        }

        public void AddRange(List<T> entities)
        {
            _context.AddRange(entities);
            _context.SaveChanges();
        }

        public void DeleteRange(List<T> entities)
        {
            _context.RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task DeleteRangeAsync(List<T> entities, CancellationToken token)
        {
            _context.RemoveRange(entities);
            await _context.SaveChangesAsync(token);
        }
    }
}
