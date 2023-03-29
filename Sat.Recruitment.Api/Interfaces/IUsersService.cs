using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Interfaces
{
    public interface IUsersService
    {
        public Task<Result> Add(User user);
        public Task<List<User>> GetAll();
    }
}
