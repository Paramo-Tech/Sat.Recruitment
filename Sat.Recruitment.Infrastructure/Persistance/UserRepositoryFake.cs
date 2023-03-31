using Sat.Recruitment.Application.Common.Interfaces.Persistance;
using Sat.Recruitment.Contracts.Results;
using Sat.Recruitment.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Persistance
{
    public class UserRepositoryFake : IUserRepository
    {
        private readonly List<User> _users;

        public UserRepositoryFake()
        {
            _users = new List<User>()
            {
                new User() { Address = "Peru 2464", Email = "Juan@marmol.com", Money = 1234, Name = "Juan", Phone = "+5491154762312", UserType = "Normal" },
                new User() { Address = "Alvear y Colombres", Email = "Franco.Perez@gmail.com", Money = 112234, Name = "Franco", Phone = "+534645213542", UserType = "Premium" },
                new User() { Address = "Garay y Otra Calle", Email = "Agustina@gmail.com", Money = 112234, Name = "Agustina", Phone = "+534645213542", UserType = "SuperUser" }
            };
        }
        public Task<Result> AddAsync(User user)
        {
            _users.Add(user);
            return Task.FromResult(new Result { IsSuccess = true, Errors = "User Created" });
        }

        public Task<List<User>> GetAllAsync()
        {
            return Task.FromResult(_users);
        }

        public Task<Result> TestAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}