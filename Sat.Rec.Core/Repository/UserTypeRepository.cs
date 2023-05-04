using Sat.Rec.Core.Infrastructure;
using Sat.Rec.Core.Repository.Interfaces;
using Sat.Rec.Models;

namespace Sat.Rec.Core.Repository
{
    public class UserTypeRepository : GenericRepository<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(DbUsersContext dbContext) : base(dbContext)
        {

        }
    }
}
