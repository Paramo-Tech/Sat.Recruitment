using Sat.Recruitment.Core.Exceptions;
using Sat.Recruitment.Core.Extensions;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Models.User;

namespace Sat.Recruitment.Services.Services
{
    /// <summary>
    /// Implemntation in memory for IUserService
    /// </summary>
    public class MemoryUserService : IUserService
    {
        private List<User> _users;

        /// <summary>
        /// Constructor
        /// </summary>
        public MemoryUserService()
        {
            _users = new List<User>();
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="user">new user</param>
        /// <returns>void</returns>
        public Task AddAsync(IUser user)
        {
            _users.Add((User)user);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>void</returns>
        public Task DeleteAsync(object id)
        {
            var user = GetAsyncById(id);
            if (user is not null)
                _users.Remove((User)user.Result);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List of users</returns>
        public Task<IEnumerable<IUser>> GetAsync()
        {
            return Task.FromResult<IEnumerable<IUser>>(_users);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User</returns>
        /// <exception cref="NotFoundException"></exception>
        public Task<IUser?> GetAsyncById(object id)
        {
            var user = _users.FirstOrDefault(x => x is not null && x.UserId is not null && x.Id.Equals(id));
            if (user is null)
                throw new NotFoundException();
            return Task.FromResult<IUser>(user);
        }
    }
}
