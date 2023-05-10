using Sat.Recruitment.BLL.Dto;
using Sat.Recruitment.BLL.interfaces;
using Sat.Recruitment.BLL.utils;
using Sat.Recruitment.DAL.Interfaces;
using Sat.Recruitment.DAL.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.BLL.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> _repository;
        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }
        public async Task<Result> CreateUser(CreateUserDTO user)
        {
            List<string> errors = Helper.ValidateErrors(user.Name, user.Email, user.Address, user.Phone);

            if (errors.Count > 0)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = string.Join(",", errors)
                };
            }

            try
            {
                var newUser = new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Address = user.Address,
                    Phone = user.Phone,
                    Money = user.Money,
                    UserType = user.UserType.ToUpper()
                };

                if (await _repository.Find(newUser))
                {
                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }

                // Prepare user to be stored
                newUser.Email = Helper.NormalizeEmail(user.Email);
                newUser.Money = Helper.CalculateGif(user.UserType, user.Money);
                // Store user    
                _repository.Create(newUser);
            }
            catch(Exception ex)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Errors = ex.Message
                };
            }

            return new Result()
            {
                IsSuccess = true,
                Errors = "User Created"
            };
        }
    }
}
