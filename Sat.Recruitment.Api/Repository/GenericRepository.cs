using Sat.Recruitment.Api.Cache.Interfaces;
using Sat.Recruitment.Api.Models.Interfaces;
using Sat.Recruitment.Api.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : IIdentifiable
    {
        protected string _cacheKey = $"{typeof(T)}";
        private readonly ICacheService _cacheService;
        public GenericRepository(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public void Update(T entity)
        {
            if (!_cacheService.TryGet(_cacheKey, out IEnumerable<T> cachedList))
            {
                throw new Exception($"Unable to find {_cacheKey} collection in memory");
            }

            var list = cachedList as List<T>;
            var index= list.FindIndex(x => x.Id == entity.Id);

            if (index > -1)
                ((List<T>)cachedList)[index] = entity;
        }


        public T Get(Func<T, bool> func)
        {
            if (!_cacheService.TryGet(_cacheKey, out ICollection<T> cachedList))
            {
                throw new Exception($"Unable to find {_cacheKey} collection in memory");
            }
            return cachedList.FirstOrDefault(func);
        }

        public IEnumerable<T> GetAll()
        {
            if (!_cacheService.TryGet(_cacheKey, out ICollection<T> cachedList))
            {
                cachedList = new List<T>();
                _cacheService.Set(_cacheKey, cachedList);
            }
            return cachedList;
        }

        public IEnumerable<T> GetAll(Func<T, bool> func)
        {
            if (!_cacheService.TryGet(_cacheKey, out ICollection<T> cachedList))
            {
                cachedList = new List<T>();
                _cacheService.Set(_cacheKey, cachedList);
            }
            return cachedList.Where(func);
        }

        public void Add(T entity)
        {
            if (!_cacheService.TryGet(_cacheKey, out ICollection<T> cachedList))
            {
                entity.Id = 1;
                cachedList = new List<T>() { entity };
                _cacheService.Set(_cacheKey, cachedList);
            }
            else
            {
                int id = cachedList.Max(x => x.Id);
                entity.Id = ++id;
                cachedList.Add(entity);
            }
        }

        public void AddRange(IEnumerable<T> entities)
        {
            int id=0;
            if (!_cacheService.TryGet(_cacheKey, out ICollection<T> cachedList))
            {
                cachedList = new List<T>();
                _cacheService.Set(_cacheKey, cachedList);
            }
            else
            {
                id = cachedList.Max(x => x.Id);
            }
            foreach (var item in entities)
            {
                item.Id = ++id;
                cachedList.Add(item);
            }

        }

        public void Remove(T entity)
        {
            if (!_cacheService.TryGet(_cacheKey, out ICollection<T> cachedList))
            {
                throw new Exception($"Unable to find {_cacheKey} collection in memory");
            }
            cachedList.Remove(entity);
        }

        public bool Any(Func<T, bool> func)
        {
            if (!_cacheService.TryGet(_cacheKey, out ICollection<T> cachedList))
            {
                throw new Exception($"Unable to find {_cacheKey} collection in memory");
            }
            return cachedList.Any(func);
        }
    }
}
