using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Repository.Users
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<List<User>> GetAllActive(CancellationToken token);
        Task<User> Remove(ulong id, CancellationToken Token);
        Task<User> Authenticate(string email, byte[] password);
    }
}
