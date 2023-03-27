using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NuGet.Frameworks;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Repositories;
using Xunit;

namespace Sat.Recruitment.Test.Controllers
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTests
    {
        private readonly UserController _userController;
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();

        public UserControllerTests()
        {
            _userController = new UserController(_mapper.Object, _userRepository.Object);
        }

        [Fact]
        public void GetAllUsers_Returns_UsersList()
        {
            _userRepository.Setup(x => x.GetAllUsers()).ReturnsAsync(new List<User>
            {
                new User
                {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.Normal,
                Money = 123
                },
                new User {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.SuperUser,
                Money = 124
                }
            });
            _mapper.Setup(x => x.Map<IEnumerable<ReadUserDTO>>(It.IsAny<IEnumerable<User>>())).Returns(new List<ReadUserDTO>
            {
                new ReadUserDTO
                {
                   Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.Normal,
                Money = 123
                },
                new ReadUserDTO {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.SuperUser,
                Money = 124
                }
            });

            var response = _userController.GetAllUsers().Result as OkObjectResult;


            Assert.Equal(2, (int)response.Value.GetType().GetProperty("Count").GetValue(response.Value));
        }

        [Fact]
        public void CreateNormalUser_CalculateGift_ForMoneyBetween10And100()
        {
            _userRepository.Setup(x => x.IsUserDuplicated(It.IsAny<User>())).ReturnsAsync(false);
            _mapper.Setup(x => x.Map<ReadUserDTO>(It.IsAny<User>())).Returns(new ReadUserDTO
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.Normal,
                Money = Convert.ToDecimal(19.8)
            });

            var newUser = new CreateUserDTO
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.Normal,
                Money = 11
            };
            var response = _userController.CreateUser(newUser);

            Assert.True(response.Result.IsSuccess);
            Assert.Equal("User Created", response.Result.Errors);
            Assert.Equal(Convert.ToDecimal(19.8), response.Result.Value.Money);
        }

        [Fact]
        public void CreateNormalUser_CalculateGift_ForMoneyGreaterThan100()
        {
            _userRepository.Setup(x => x.IsUserDuplicated(It.IsAny<User>())).ReturnsAsync(false);
            _mapper.Setup(x => x.Map<ReadUserDTO>(It.IsAny<User>())).Returns(new ReadUserDTO
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.Normal,
                Money = Convert.ToDecimal(113.12)
            });

            var newUser = new CreateUserDTO
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.Normal,
                Money = 101
            };
            var response = _userController.CreateUser(newUser);

            Assert.True(response.Result.IsSuccess);
            Assert.Equal("User Created", response.Result.Errors);
            Assert.Equal(Convert.ToDecimal(113.12), response.Result.Value.Money);
        }

        [Fact]
        public void CreateSuperUserUser_CalculateGift_ForMoneyGreaterThan100()
        {
            _userRepository.Setup(x => x.IsUserDuplicated(It.IsAny<User>())).ReturnsAsync(false);
            _mapper.Setup(x => x.Map<ReadUserDTO>(It.IsAny<User>())).Returns(new ReadUserDTO
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.Normal,
                Money = Convert.ToDecimal(121.2)
            });

            var newUser = new CreateUserDTO
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.SuperUser,
                Money = 101
            };
            var response = _userController.CreateUser(newUser);

            Assert.True(response.Result.IsSuccess);
            Assert.Equal("User Created", response.Result.Errors);
            Assert.Equal(Convert.ToDecimal(121.2), response.Result.Value.Money);
        }

        [Fact]
        public void CreatePremiumUser_CalculateGift_ForMoneyGreaterThan100()
        {
            _userRepository.Setup(x => x.IsUserDuplicated(It.IsAny<User>())).ReturnsAsync(false);
            _mapper.Setup(x => x.Map<ReadUserDTO>(It.IsAny<User>())).Returns(new ReadUserDTO
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.Normal,
                Money = Convert.ToDecimal(303)
            });

            var newUser = new CreateUserDTO
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.Premium,
                Money = 101
            };
            var response = _userController.CreateUser(newUser);

            Assert.True(response.Result.IsSuccess);
            Assert.Equal("User Created", response.Result.Errors);
            Assert.Equal(Convert.ToDecimal(303), response.Result.Value.Money);
        }

        [Fact]
        public void CreateUser_ReturnSuccess_When_UserCreated()
        {
            _userRepository.Setup(x => x.IsUserDuplicated(It.IsAny<User>())).ReturnsAsync(false);
            var response = _userController.CreateUser(new CreateUserDTO
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.Normal,
                Money = 124
            });

            Assert.True(response.Result.IsSuccess);
            Assert.Equal("User Created", response.Result.Errors);
        }

        [Fact]
        public void CreateUser_ReturnError_When_UserIsDuplicated()
        {
            _userRepository.Setup(x => x.IsUserDuplicated(It.IsAny<User>())).ReturnsAsync(true);
            var result = _userController.CreateUser(new CreateUserDTO
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.Normal,
                Money = 124
            }).Result;


            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Fact]
        public void CreateUser_ValidateModel_ThrowValidationError()
        {
            var createUserDTO = new CreateUserDTO
            {
                Name = "Agustina",
                Email = "Agustinagmail.com",
                Address = "Av. Juan G",
                Phone = "+3491122354215",
                UserType = UserType.Normal,
                Money = 124
            };
            var result = _userController.CreateUser(createUserDTO).Result;

            Assert.Contains(ValidateModel(createUserDTO), v => v.MemberNames.Contains("Email") && v.ErrorMessage.Contains("Invalid email address"));            
        }


        [Fact]
        public void CreateUser_Returns_IsSuccessFalse_WhenInvalidModel()
        {
            var createUserDTO = new CreateUserDTO
            {
              
            };
            var result = _userController.CreateUser(createUserDTO).Result;

            Assert.False(result.IsSuccess);
            Assert.Equal("Object reference not set to an instance of an object.", result.Errors);
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
