using Application.Models;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IUserService
    {
        Task<Result> Create(UserCreationDto userCreationDto);
    }
}
