using Domain.Entities;

namespace Application.InterfacesApplication
{
    public interface IUserUseCase
    {
        bool CreateUser(UserDomain user);
    }
}
