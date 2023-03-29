using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        protected readonly IBaseItem<T> _baseItem;

        public ServiceBase(IBaseItem<T> baseItem)
        {
            _baseItem = baseItem;
        }

        public virtual async Task<Result> AddItemAsync(T item)
        {
            return await _baseItem.AddAsync(item);
        }

        public virtual async Task<List<User>> GetAllAsync()
        {
            return await _baseItem.GetAllAsync();
        }
    }
}
