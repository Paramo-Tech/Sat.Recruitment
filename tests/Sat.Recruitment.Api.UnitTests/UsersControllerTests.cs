using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.MappingProfiles;
using Sat.Recruitment.Core.Abstractions.Services;
using Sat.Recruitment.Core.DomainEntities;
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
