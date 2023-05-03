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
            var userExist = await _userRepository.GetUserByName(user.Name, user.Email);
            if (userExist != null) throw new ApplicationException("user exists");
            var email = ValidateEmail(user.Email);
            user.Email = email;
            user.Money = user.calculateMoney.CalculateAllocationToUser(user.Money);
            var result = await _userRepository.Create(user);
            if (result) return true;
            return false;
        }

        public async Task<IUserType> GetUser(int id)
        {
            var userDomain = await _userRepository.GetUserById(id);
            if (userDomain == null)
                throw new KeyNotFoundException("user not found");
            return userDomain;
        }

        public string ValidateEmail(string email)
        {
            if (email == null) throw new NullReferenceException("email Null");
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            email = string.Join("@", new string[] { aux[0], aux[1] });

            return email;
        }
    }
}
