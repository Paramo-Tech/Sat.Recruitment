using System.Threading.Tasks;
using Core.CQS.Commands;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<bool> Exists(CreateNewUserDTO dto);
    }
}
