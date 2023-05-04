using Microsoft.EntityFrameworkCore;
using Sat.Rec.Core.Infrastructure;
using Sat.Rec.Core.Repository.Interfaces;
using Sat.Rec.Models;

namespace Sat.Rec.Core.Repository
{
    public class GIFUserTypeRepository : GenericRepository<GIFUserType>, IGIFUserTypeRepository
    {
        public GIFUserTypeRepository(DbUsersContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<GIFUserType>> GetAllByUserTypeId(int userTypeId)
        {
            return await _dbContext.Set<GIFUserType>().Where(x=> x.UserTypeId == userTypeId).ToListAsync();
        }
    }
}
