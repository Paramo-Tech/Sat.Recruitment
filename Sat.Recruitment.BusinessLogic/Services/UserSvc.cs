using Sat.Recruitment.BusinessLogic.ExternalServices;
using Sat.Recruitment.Models.Models;
using Sat.Recruitment.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.BusinessLogic.Services
{
    public class UserSvc : IUserSvc
    {
        private readonly IUserRepository _userRepository;
        public UserSvc(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Get(Guid Id)
        {
            return await _userRepository.Get(Id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<Result> Save(UserModel user)
        {
            try
            {
                if (!await IsUserInDB(user))
                {
                    if (await _userRepository.Save(new User(user)))
                    {
                        return new Result()
                        {
                            IsSuccess = true,
                            Message = "User created successfully!"
                        };
                    }
                    else
                    {
                        return new Result()
                        {
                            IsSuccess = false,
                            Message = "Ooops! Something has gone wrong..."
                        };
                    }
                }
                else
                {
                    return new Result()
                    {
                        IsSuccess = false,
                        Message = "The user is duplicated"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        private async Task<bool> IsUserInDB(UserModel userInput)
        {
            var userList = await _userRepository.GetAll();
            if (userList.Any())
            {
                if (userList.Where(u => u.Email == userInput.Email || u.Phone == userInput.Phone).Any())
                    return true;
                else if (userList.Where(u => u.Name.ToUpper() == userInput.Name.ToUpper() &&
                                            u.Address.ToUpper() == userInput.Address.ToUpper()).Any())
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
