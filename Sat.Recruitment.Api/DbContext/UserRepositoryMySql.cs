using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Sat.Recruitment.Api.Models;

    public class UserRepositoryMySql : IRepository<User>
    {
        private readonly MyDbContext _context;

        public UserRepositoryMySql(MyDbContext context)
        {
            _context = context;
        }

        public void Add(User entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(User entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public User GetById(int id)
        {
            return _context.Set<User>().Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Set<User>().ToList();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public bool Exists(User entity)
        {
            return _context.Set<User>().Where(u => (u.Email == entity.Email || u.Phone == entity.Phone) || ((u.Name == entity.Name && u.Address == entity.Address))).ToList().Count() > 0;
        }
    }

}
