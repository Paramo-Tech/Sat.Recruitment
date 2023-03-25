using Sat.Recruitment.Domain.Models.Users;
using Sat.Recruitment.Domain.Results;

namespace Sat.Recruitment.DataAccess.Contract.Users
{
    public interface IUserDataAccess : ICRUDItem<User>
    {
        Task<ExecutionResult> CheckDuplicates(User user);
    }
}
