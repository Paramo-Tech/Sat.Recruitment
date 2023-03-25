using FluentValidation;
using Sat.Recruitment.DataAccess.Contract;
using Sat.Recruitment.Domain.Contract;
using Sat.Recruitment.Domain.Results;

namespace Sat.Recruitment.Application.Services
{
    public class ServiceBase<T> : IServiceBase<T>
        where T  : class
    {
        protected readonly IValidator<T> _validator;
        protected readonly ICRUDItem<T> _itemDataAccess;

        public ServiceBase(ICRUDItem<T> itemDataAccess, IValidator<T> validator) 
        {
            _validator = validator;
            _itemDataAccess = itemDataAccess;
        }

        /// <summary>
        /// Validate an items and adds it to the DB
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual async Task<ExecutionResult> AddItemAsync(T item)
        {
            var validationBase = await ValidateItemAsync(item);
            if(!validationBase.IsSuccess)
            {
                return validationBase;
            }
            return await _itemDataAccess.AddAsync(item);
        }

        public virtual async Task<ExecutionResult> DeleteItemAsync(T item)
        {
            return await Task.FromResult(new ExecutionResult { IsSuccess = true, Errors = "User deleted" });
        }

        public virtual async Task<List<T>> GetItemsAsync()
        {
            return await _itemDataAccess.GetItemsAsync();
        }

        public virtual async Task<ExecutionResult> UpdateItemAsync(T item)
        {
            return await Task.FromResult(new ExecutionResult { IsSuccess = true, Errors ="User Updated" });
        }


        /// <summary>
        /// Validates the item fields
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual async Task<ExecutionResult> ValidateItemAsync(T item)
        {
            var valid = await _validator.ValidateAsync(item);
            return new ExecutionResult() { IsSuccess = valid.IsValid };
        }
    }
}
