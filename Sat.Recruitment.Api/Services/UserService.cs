using Sat.Recruitment.Api.Models.DTOs;
using Sat.Recruitment.Api.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserTypeRepository _userTypeRepository;

        public UserService(IUserRepository userRepository, IUserTypeRepository userTypeRepository)
        {
            _userRepository = userRepository;
            _userTypeRepository = userTypeRepository;
        }

        public async Task<UserModelDto> CreateUpdateUser(UserModelDto userDto)
        {
            var _userDto = await _userRepository.CreateUpdate(userDto);

            return _userDto;
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            bool delete = await _userRepository.Delete(userId);

            return delete;
        }

        public async Task<UserModelDto> GetUserById(Guid userId)
        {
            var _userDto = await _userRepository.GetById(userId);

            return _userDto;
        }

        public async Task<IEnumerable<UserModelDto>> GetUsers()
        {
            var _userList = await _userRepository.Get();
            return _userList;
        }

        public async Task<string> GetUserTypeById(Guid typeId)
        {
            var _type = await _userTypeRepository.GetTypeById(typeId);

            return _type;
        }

        public async Task<UserModelDto> SaveUpdateUser(UserModelDto userDto)
        {
            string findTypeUser = await _userTypeRepository.GetTypeById(userDto.UserTypeId);
            
            userDto.Money = SetMoney(Convert.ToDecimal(userDto.Money), findTypeUser);

            return await CreateUpdateUser(userDto);
        }

        public decimal SetMoney(decimal _money, string userType)
        {
            var money = Convert.ToDecimal(0);

            switch (userType)
            {
                case Constants.UserTypes.SuperUser:

                    if (_money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        var gif = _money * percentage;
                        money = _money + gif;
                    }
                    break;

                case Constants.UserTypes.Premium:

                    if (_money > 100)
                    {
                        var gif = _money * 2;
                        money = _money + gif;
                    }
                    break;

                default:

                    //Normal
                    if (_money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        var gif = _money * percentage;
                        money += gif;
                    }
                    else if (_money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = _money * percentage;
                        money += gif;
                    }
                break;
            }
            return money;
        }

        public async Task<bool> IsUserDuplicate(UserModelDto userDto)
        {
            return await _userRepository.FindDuplicate(userDto);
        }

        public async Task<IEnumerable<UserTypeModelDto>> GetUsersTypes()
        {
            var _userList = await _userTypeRepository.Get();
            return _userList;
        }
    }
}
