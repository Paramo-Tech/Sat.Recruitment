using Sat.Rec.Models;

namespace Sat.Rec.Core.Repository.Interfaces
{
    public interface IGIFUserTypeRepository : IGenericRepository<GIFUserType>
    {
        Task<IEnumerable<GIFUserType>> GetAllByUserTypeId(int userTypeId);
    }
}
