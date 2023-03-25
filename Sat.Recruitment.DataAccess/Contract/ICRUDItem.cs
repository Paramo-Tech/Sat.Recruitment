using Sat.Recruitment.Domain.Results;

namespace Sat.Recruitment.DataAccess.Contract
{
    public interface ICRUDItem<T>
        where T : class
    {
        public Task<ExecutionResult> AddAsync(T user);
        public Task UpdateAsync(T user);
        public Task DeleteAsync(T user);
        public Task<List<T>> GetItemsAsync();
    }
}
