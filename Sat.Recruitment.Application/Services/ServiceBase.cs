using Sat.Recruitment.Application.Common.Interfaces.Persistance;
using Sat.Recruitment.Application.Services.Interfaces;
using Sat.Recruitment.Contracts.Results;
using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        protected readonly IRepositoryBase<T> _repoBase;

        public ServiceBase(IRepositoryBase<T> repoBase)
        {
            _repoBase = repoBase;
        }

        public virtual async Task<Result> AddItemAsync(T item)
        {
            return await _repoBase.AddAsync(item);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _repoBase.GetAllAsync();
        }
    }
}
