using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using System.Threading.Tasks;

namespace Application.InterfacesApplication
{
    public interface IUserUseCase
    {
        Task<bool> CreateUser(IUserType user);
        IUserType CreateUserDomain(string name, string email, string address, string phone, string userType, decimal money);

        Task<IUserType> GetUser(int id);
    }
}
