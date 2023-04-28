using Domain.Entities;
using Domain.Enums;
using System.Threading.Tasks;

namespace Application.InterfacesApplication
{
    public interface IUserUseCase
    {
        Task<bool> CreateUser(UserDomain user);
        UserDomain CreateUserDomain(string name, string email, string address, string phone, string userType, decimal money);
    }
}
