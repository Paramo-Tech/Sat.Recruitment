using Sat.Recruitment.Application.Common.Interfaces.Persistance;
using Sat.Recruitment.Contracts.Results;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Infrastructure.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Persistance
{
    public class UserRepository : IUserRepository
    {
        public Task<Result> AddAsync(User user)
        {
            FileContext.ListUsers.Add(user);
            FileHandler.AppendUserToFile(user);
            return Task.FromResult(new Result { IsSuccess = true, Errors = "User Created" });
        }

        public Task<List<User>> GetAllAsync()
        {
            FileHandler.ReadUsersFromFile();
            return Task.FromResult(FileContext.ListUsers);            
        }
    }
}
