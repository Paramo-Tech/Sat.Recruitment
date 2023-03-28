using AutoMapper;
using Sat.Recruitment.Application.BusinessLogic;
using Sat.Recruitment.Application.DTOs;
using Sat.Recruitment.Application.Interfaces.Repositories;
using Sat.Recruitment.Application.Interfaces.Services;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Exceptions;
using Sat.Recruitment.Domain.Exceptions.Messages;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserBL _userBL;
        private readonly IUserRepository _userRepository;
        private IMapper _mapper { get; set; }

        public UserService(IUserBL userBL, IUserRepository userRepository, IMapper mapper)
        {
            this._userBL = userBL;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public void CreateNewUser(User user)
        {
            _userBL.ValidateGift(user);
            _userBL.ValidateEmail(user);
            var allUser = _userRepository.GetAll();
            if (allUser.Any(u => u.Email == user.Email))
                throw new DuplicatedUserException(ExceptionMessages.DuplicatedUserEmail);

            if (allUser.Any(u => u.Phone == user.Phone))
                throw new DuplicatedUserException(ExceptionMessages.DuplicatedUserPhone);

            if (allUser.Any(u => u.Name == user.Name && u.Address == user.Address))
                throw new DuplicatedUserException(ExceptionMessages.DuplicatedUserNameAndAddress);

            _userRepository.Save(user);
        }

        public List<UserDTO> GetUsers()
        {
            List<UserDTO> result = new List<UserDTO>();
            _mapper.Map(_userRepository.GetAll(), result);
            return result;
        }
    }
}
