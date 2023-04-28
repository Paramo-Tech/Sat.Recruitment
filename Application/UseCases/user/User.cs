using Application.Interfaces;
using Application.InterfacesApplication;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.user
{
    public class User : IUserUseCase
    {
        private readonly IUserRepository _userRepository;
        public User(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDomain CreateUserDomain(string name, string email, string address, string phone, string userType, decimal money)
        {
            return new UserDomain() { Name = name, Email = email, Address = address, Phone = phone, UserType = Enum.Parse<UserType>(userType), Money = 2000 };
        }

        public async Task<bool> CreateUser(UserDomain user)
        {
            //to do calculate with usertype
            //set money according with the calculate


            await _userRepository.Create(user);



            return true;
        }
    }
}
