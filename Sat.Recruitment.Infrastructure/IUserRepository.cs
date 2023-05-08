using Sat.Recruitment.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure
{
    // REMARKS: Ideally repository should be generic to support reutilization, but given how the textfile manipulation is specific
    // I've decided to turn down the generic approach in this case.
    
    /// <summary>
    /// Contains all the necesary operations to manipulate with Users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets all users from the current data source.
        /// </summary>
        /// <returns>Returns a list of users.</returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Adds an user to the data source.
        /// </summary>
        /// <param name="entity">Object containing the information of the new user.</param>
        /// <returns>Returns the user just created.</returns>
        Task<User> AddAsync(User entity);
    }
}
