using Sat.Recruitment.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Contracts.Services
{
    public  interface IUserService
    {
        /// <summary>
        /// Add a new user in async way 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddAsync(User user);

        /// <summary>
        /// Checks if a new user is in the user's list in an async way.
        /// </summary>
        /// <param name="user">new user</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(User user);
        
        /// <summary>
        /// Retrieves all the users in the list in an async way.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<User>> GetUsersAsync();
    }
}
