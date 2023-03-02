using Sat.Recruitment.Api.Cache.Interfaces;
using Sat.Recruitment.Api.Models.Interfaces;
using Sat.Recruitment.Api.Repository.Interfaces;

namespace Sat.Recruitment.Api.Repository
{
    public class UserRepository : GenericRepository<IUser>, IUserRepository
    {
        
        public UserRepository(ICacheService cacheService) : base(cacheService)
        {
            _cacheKey = "Users";
        }
    }
}
