using Sat.Recruitment.Core.Models.User;

namespace Sat.Recruitment.Core.Interfaces
{
    public interface IUserService
    {
        Task AddAsync(IUser user);

        Task DeleteAsync(object id);

        Task<IEnumerable<IUser>> GetAsync();

        Task<IUser?> GetAsyncById(object id);
    }
}