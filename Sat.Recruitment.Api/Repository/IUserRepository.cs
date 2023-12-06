using Sat.Recruitment.Api.Models;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Repository
{
    public interface IUserRepository
    {
        public List<User> GetUsers(); 
    }
}
