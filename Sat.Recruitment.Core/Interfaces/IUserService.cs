using Sat.Recruitment.Core.Entities;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.Interfaces
{
    public interface IUserService
    {
        Task<string> Create(USER request);
    }
}