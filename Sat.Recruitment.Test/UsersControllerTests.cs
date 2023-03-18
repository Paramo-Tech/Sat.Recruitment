using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Business;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("UsersControllerTests", DisableParallelization = true)]
    public class UsersControllerTests
    {
        private Mock<IUserBusiness> _userBusinessMock;
        private Mock<IMapper> _mapper;
        private Mock<ILogger<UsersController>> _logger;
        public UsersControllerTests()
        {
            _userBusinessMock = new Mock<IUserBusiness>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<UsersController>>();
        }

        [Fact]
        public async Task UserShouldBeCreatedWhenDataIsCorrectAndNotDuplicated ()
        {
            UserRequest request = new UserRequest
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike@gmail.com",
                Money = "1000",
                Name = "Mike",
                UserType = "Normal"
            };
            User userRequestMapped = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike@gmail.com",
                Money = 1000,
                Name = "Mike",
                UserType = "Normal"
            };
            User response = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike@gmail.com",
                Money = 1120,
                Name = "Mike",
                UserType = "Normal"
            };
            UserRequest responseMapped = new UserRequest
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike@gmail.com",
                Money = "1120",
                Name = "Mike",
                UserType = "Normal"
            };
            _userBusinessMock.Setup(s => s.CreateUser(userRequestMapped)).ReturnsAsync(response);
            _mapper.Setup(m => m.Map<User>(request)).Returns(userRequestMapped);
            _mapper.Setup(m => m.Map<UserRequest>(response)).Returns(responseMapped);

            var userController = new UsersController(_userBusinessMock.Object,_mapper.Object,_logger.Object);

            ActionResult<UserRequest> result = await userController.CreateUser(request);
            Assert.NotNull(result);
            Assert.Equal(responseMapped, result.Value);
        }

        [Fact]
        public async Task UserShouldNotBeCreatedWhenDataIsDuplicated()
        {
            UserRequest request = new UserRequest
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike@gmail.com",
                Money = "1000",
                Name = "Mike",
                UserType = "Normal"
            };
            User userRequestMapped = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike@gmail.com",
                Money = 1000,
                Name = "Mike",
                UserType = "Normal"
            };
            User response = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike@gmail.com",
                Money = 1120,
                Name = "Mike",
                UserType = "Normal"
            };
            UserRequest responseMapped = new UserRequest
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike@gmail.com",
                Money = "1120",
                Name = "Mike",
                UserType = "Normal"
            };
            _userBusinessMock.Setup(s => s.CreateUser(userRequestMapped)).ThrowsAsync(new DuplicateNameException("User duplicated"));
            _mapper.Setup(m => m.Map<User>(request)).Returns(userRequestMapped);
            _mapper.Setup(m => m.Map<UserRequest>(response)).Returns(responseMapped);

            var userController = new UsersController(_userBusinessMock.Object, _mapper.Object, _logger.Object);

            ActionResult<UserRequest> result = await userController.CreateUser(request);
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
