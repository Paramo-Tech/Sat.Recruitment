using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class UserController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("/get-all-users")]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(_mapper.Map<IEnumerable<ReadUserDTO>>(users));            
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result<ReadUserDTO>> CreateUser(CreateUserDTO createUserDTO)
        {
            try
            {
                var newUser = new User
                {
                    Name = createUserDTO.Name,
                    Email = createUserDTO.Email,
                    Address = createUserDTO.Address,
                    Phone = createUserDTO.Phone,
                    UserType = createUserDTO.UserType,
                    Money = createUserDTO.Money
                };
                CalculateGiftByUserType(newUser);
                NormalizeEmail(newUser);
                var isDuplicated = await _userRepository.IsUserDuplicated(newUser);

                if (!isDuplicated)
                {
                    _userRepository.CreateUser(newUser);
                    var readUserDTO = _mapper.Map<ReadUserDTO>(newUser);
                    return new Result<ReadUserDTO>()
                    {
                        Value = readUserDTO,
                        IsSuccess = true,
                        Errors = "User Created"
                    };
                }
                else
                {
                    var readUserDTO = _mapper.Map<ReadUserDTO>(newUser);
                    return new Result<ReadUserDTO>()
                    {
                        Value = readUserDTO,
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }
            }
            catch (Exception ex) 
            {
                return new Result<ReadUserDTO>()
                {
                    Value = null,
                    IsSuccess = false,
                    Errors = $"{ex.Message}"
                };
            }
        }

        private static void CalculateGiftByUserType(User newUser)
        {
            switch (newUser.UserType)
            {
                case UserType.Normal:
                    CalculateGiftForNormalUser(newUser);                    
                    break;
                case UserType.SuperUser:
                    CalculateGiftForSuperUser(newUser);
                    break;
                case UserType.Premium:
                    CalculateGiftForPremiumUser(newUser);
                    break;
                default:
                    throw new ArgumentException("Invalid User Type.", nameof(newUser.UserType));
            }
        }

        private static void CalculateGiftForPremiumUser(User newUser)
        {
            int premiumLevel = 100;
            if (newUser.Money > premiumLevel)
            {
                CalculateGift(newUser, 2);
            }
        }

        private static void CalculateGiftForSuperUser(User newUser)
        {
            int superLevel = 100;
            if (newUser.Money > superLevel)
            {
                CalculateGift(newUser, Convert.ToDecimal(0.2));
            }
        }

        private static void CalculateGiftForNormalUser(User newUser)
        {
            int normalLowLevel = 10;
            int normalTopLevel = 100;
            if (newUser.Money > normalLowLevel && newUser.Money < normalTopLevel)
            {
                CalculateGift(newUser, Convert.ToDecimal(0.8));
            }
            else if (newUser.Money > normalTopLevel)
            {
                CalculateGift(newUser, Convert.ToDecimal(0.12));
            }
        }

        private static void CalculateGift(User newUser, decimal percentage)
        {
            var gift = newUser.Money * percentage;
            newUser.Money = newUser.Money + gift;
        }

        private static void NormalizeEmail(User newUser)
        {
            var atIndex = newUser.Email.IndexOf("+");
            if (atIndex >= 0)
            {
                newUser.Email = newUser.Email.Substring(0, atIndex).Replace(".", "") + newUser.Email.Substring(newUser.Email.IndexOf("@"), newUser.Email.Length - newUser.Email.IndexOf("@"));
            }
            else
            {
                newUser.Email = newUser.Email.Replace(".", "");
            }
        }
    }
}
