using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.DTOs.Users;
using Sat.Recruitment.Api.MappingProfiles;
using Sat.Recruitment.Core.Abstractions.Services;
using Sat.Recruitment.Core.DomainEntities;
using Sat.Recruitment.Core.Enums;
using Sat.Recruitment.Core.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Api.UnitTests
{
    public class UsersControllerTests
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsersControllerTests()
        {
            // Logger
            this._logger = new Mock<ILogger<UsersController>>().Object;

            // Mapper
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainEntitiesMappingProfile());
            });
            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async Task Create_WithUnexistingUser_ReturnsCreatedAtActionResultWithCreateResponse()
        {
            // Arrange
            User createdUser = new User() // Irrelevant content, but needs to be correct
            {
                Id = Guid.Parse("999e0cf1-d9e0-498f-ba42-8a9a8f402459"),
                Name = "Testing User",
                Email = "testinguser@domain.com",
                Address = "Testing User 123",
                Phone = "+54123123123",
                UserType = UserType.Normal,
                Money = 100
            };

            CreateRequest createRequest = new CreateRequest() // Irrelevant content, but needs to be correct
            {
                Name = "Testing User",
                Email = "testinguser@domain.com",
                Address = "Testing User 123",
                Phone = "+54123123123",
                UserType = UserType.Normal,
                Money = 100
            };

            CreateResponse expectedCreateResponse = new CreateResponse() // Irrelevant content, but needs to be correct
            {
                Id = Guid.Parse("999e0cf1-d9e0-498f-ba42-8a9a8f402459"),
                Name = "Testing User",
                Email = "testinguser@domain.com",
                Address = "Testing User 123",
                Phone = "+54123123123",
                UserType = UserType.Normal,
                Money = 100
            };

            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(userService => userService.Create(It.IsAny<User>())).ReturnsAsync(createdUser);

            var controller = new UsersController(_logger, _mapper, userServiceStub.Object);

            // Act
            var result = await controller.Create(createRequest);
            CreateResponse obtainedCreateResponse = (result.Result as CreatedAtActionResult).Value as CreateResponse;

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(expectedCreateResponse.Id, obtainedCreateResponse.Id);
            Assert.Equal(expectedCreateResponse.Name, obtainedCreateResponse.Name);
            Assert.Equal(expectedCreateResponse.Email, obtainedCreateResponse.Email);
            Assert.Equal(expectedCreateResponse.Address, obtainedCreateResponse.Address);
            Assert.Equal(expectedCreateResponse.Phone, obtainedCreateResponse.Phone);
            Assert.Equal(expectedCreateResponse.UserType, obtainedCreateResponse.UserType);
            Assert.Equal(expectedCreateResponse.Money, obtainedCreateResponse.Money);
        }

        [Fact]
        public async Task GetById_WithExistingUser_ReturnsOk()
        {
            // Arrange
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(userService => userService.GetById(It.IsAny<Guid>())).ReturnsAsync(new User());

            var controller = new UsersController(_logger, _mapper, userServiceStub.Object);

            // Act
            var result = await controller.GetById(Guid.NewGuid());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_WithUnexistingUser_ReturnsNotFound()
        {
            // Arrange
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(userService => userService.GetById(It.IsAny<Guid>())).Throws(new EntityNotFoundException(typeof(User).Name));

            var controller = new UsersController(_logger, _mapper, userServiceStub.Object);

            // Act
            var result = await controller.GetById(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
