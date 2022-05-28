using System.Collections.Generic;
using System.Threading.Tasks;
using Sat.Recruitment.Api.DataAccess.DataObjects;

namespace Sat.Recruitment.Api.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<IList<UserModelDto>> GetAllAsync();
    }
}