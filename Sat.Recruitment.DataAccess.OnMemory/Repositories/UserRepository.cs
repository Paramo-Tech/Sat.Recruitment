using Sat.Recruitment.DataAccess.Contract.Users;
using Sat.Recruitment.DataAccess.OnMemory.DataBase;
using Sat.Recruitment.Domain.Models.Users;
using Sat.Recruitment.Domain.Results;

namespace Sat.Recruitment.DataAccess.OnMemory.Repositories
{
    /*
        NOTE:
        I did all functions to be prepared for Async, but for this example i'm not using async functions 
     */
    public class UserRepository : IUserDataAccess
    {
        public Task<ExecutionResult> AddAsync(User user)
        {
            OnMemoryDataBase.Users.Add(user);
            return Task.FromResult(new ExecutionResult() { Errors = "User Created", IsSuccess = true });
        }

        public Task<ExecutionResult> CheckDuplicates(User user)
        {
            var duplicates = OnMemoryDataBase.Users.Where(
                    u=> (u.Email == user.Email || u.Phone== user.Phone) 
                    || 
                    (u.Name == user.Name && u.Address == user.Address)
                ).Count();

            if(duplicates > 0)
            {
                return Task.FromResult(new ExecutionResult() { IsSuccess = false, Errors = "The user is duplicated" });
            }

            return Task.FromResult(new ExecutionResult() { IsSuccess = true, Errors = "User Created" });
        }

        public Task DeleteAsync(User user)
        {
            return Task.CompletedTask;
        }

        public Task<List<User>> GetItemsAsync()
        {
            return Task.FromResult(OnMemoryDataBase.Users);
        }

        public Task UpdateAsync(User user)
        {
            return Task.CompletedTask;
        }
    }
}
