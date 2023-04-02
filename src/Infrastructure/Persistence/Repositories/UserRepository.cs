using Application.Contracts;
using Application.Contracts.Repositories;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task CreateAsync(User user)
        {
            DataAccess.AppendUser(user);

            return Task.CompletedTask;
        }

        public Task<List<User>> GetAllAsync()
        {
            var users = DataAccess.GetUsers();

            return Task.FromResult(users);
        }
    }
}