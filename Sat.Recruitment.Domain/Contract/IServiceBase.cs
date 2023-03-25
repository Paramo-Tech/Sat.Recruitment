using Sat.Recruitment.Domain.Results;

namespace Sat.Recruitment.Domain.Contract
{
    public interface IServiceBase<T>
    {
        Task<ExecutionResult> ValidateItemAsync(T item);
        Task<ExecutionResult> AddItemAsync(T item);
        Task<ExecutionResult> UpdateItemAsync(T item);
        Task<ExecutionResult> DeleteItemAsync(T item);
        Task<List<T>> GetItemsAsync();
    }
}
