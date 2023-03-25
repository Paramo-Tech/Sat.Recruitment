using Sat.Recruitment.DataAccess.Contract.OtherClass;
using Sat.Recruitment.Domain.Models.OtherClass;
using Sat.Recruitment.Domain.Results;

namespace Sat.Recruitment.DataAccess.OnMemory.Repositories
{
    public class ExtraClassRepository : IExtraClassDataAccess
    {
        public Task<ExecutionResult> AddAsync(ExtraClass user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ExtraClass user)
        {
            throw new NotImplementedException();
        }

        public Task<List<ExtraClass>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ExtraClass user)
        {
            throw new NotImplementedException();
        }
    }
}
