using Sat.Recruitment.Api.Models.DTO;
using Sat.Recruitment.Api.Models.Interfaces;
using Sat.Recruitment.Api.Repository.Interfaces;
using Sat.Recruitment.Api.Services.Interfaces;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserFactory _factory;
        private readonly IUserRepository _repository;

        public UserService(IUserFactory factory, IUserRepository repository)
        {
            this._factory = factory;
            this._repository = repository;
        }

        public void CreateUser(UserDTO dto)
        {
            IUser newUser = _factory.CreateUser(dto);

            newUser.Money = newUser.Money + newUser.Gift;
            _repository.Add(newUser);
        }
    }
}
