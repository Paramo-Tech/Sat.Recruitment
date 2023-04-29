using Application.Interfaces;
using Application.InterfacesApplication;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
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

        public IUserType CreateUserDomain(string name, string email, string address, string phone, string userType, decimal money)
        {
            switch (userType)
            {
                case "Normal":
                    return new UserDomain() { Name = name, Email = email, Address = address, Phone = phone, UserType = Enum.Parse<UserType>(userType), Money = money };

                case "SuperUser":
                    return new SuperDomain() { Name = name, Email = email, Address = address, Phone = phone, UserType = Enum.Parse<UserType>(userType), Money = money };
                case "Premium":
                    return new PremiumDomain() { Name = name, Email = email, Address = address, Phone = phone, UserType = Enum.Parse<UserType>(userType), Money = money };
                default:
                    break;
            }
            return new UserDomain() { Name = name, Email = email, Address = address, Phone = phone, UserType = Enum.Parse<UserType>(userType), Money = money };

        }

        public async Task<bool> CreateUser(IUserType user)
        {
            //open/close principle and polymorphism
            user.Money = user.calculateMoney.CalculateAllocationToUser(user.Money);

            await _userRepository.Create(user);

            return true;
        }
    }
}
