using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.DTO;
using Sat.Recruitment.Api.Models.Validators;
using Sat.Recruitment.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Controllers
{
    [CollectionDefinition("ControllersTest", DisableParallelization = true)]
    public class UsersControllerTest
    {
        private Mock<IValidator<UserDTO>> _validatorMock;
        private Mock<IUserService> _userServiceMock;
        
        private UserDTO Dto => new UserDTO()
        {
            Name = "Mike",
            Email = "mike@gmail.com",
            Address = "Av. Juan G",
            Phone = "+349 1122354215",
            UserType = UserTypes.NORMAL,
            Money = 124
        };


        public UsersControllerTest()
        {
            _validatorMock = new Mock<IValidator<UserDTO>>();
            _userServiceMock = new Mock<IUserService>();
        }

        [Fact]
        public void Post_should_return_ok()
        {
            _validatorMock.Setup(x => x.Validate(It.IsAny<UserDTO>())).Returns(new ValidationResult());
            var userController = new UsersController(_userServiceMock.Object,_validatorMock.Object);
            UserDTO dto = Dto;
            
            var result = userController.CreateUser(dto).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal("User Created", result.Value.ToString());
        }

        [Fact]
        public void Post_should_return_user_duplicated()
        {
            _validatorMock.Setup(x => x.Validate(It.IsAny<UserDTO>()))
                .Returns(new ValidationResult(new[] { new ValidationFailure("Email", "The user is duplicated") }));
            var userController = new UsersController(_userServiceMock.Object, _validatorMock.Object);

            UserDTO dto = Dto;

           
            var result = userController.CreateUser(dto).Result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal("The user is duplicated", result.Value.ToString());
        }


        [Fact]
        public void Post_should_return_value_is_required()
        {
            _validatorMock.Setup(x => x.Validate(It.IsAny<UserDTO>()))
                .Returns(new ValidationResult(new[] { new ValidationFailure("Address", "Address is required") }));
            var userController = new UsersController(_userServiceMock.Object, _validatorMock.Object);

            UserDTO dto = Dto;


            var result = userController.CreateUser(dto).Result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal("Address is required", result.Value.ToString());
        }
    }
}
