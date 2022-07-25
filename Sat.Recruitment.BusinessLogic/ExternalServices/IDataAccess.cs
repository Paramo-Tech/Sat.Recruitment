using Sat.Recruitment.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.BusinessLogic.ExternalServices
{
    public interface IDataAccess
    {
        Task<List<User>> ReadData();
        Task<bool> SaveData(List<User> objList);
    }
}