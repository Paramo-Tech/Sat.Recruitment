using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sat.Recruitment.Data.Definitions;
using Sat.Recruitment.Domain.Domains;
using Sat.Recruitment.Services.Definitions;
using Sat.Recruitment.Services.Implementations.UsersType;

namespace Sat.Recruitment.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<Result> ProcessingNewUser(User newUser)
        {
            var userNormal = new Normal();
            var userPremium = new Premium();
            var userSuperUser = new SuperUser();
            AbstractHandler handler = userNormal;
            newUser = handler.Handle(newUser);
            return _userRepository.ProcessingNewUserAsync(newUser);
        }

    }
}
