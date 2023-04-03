using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Data.Repositories;
using Sat.Recruitment.Api.Models;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UsersControllerTest
    {
        [Fact]
        async void CreateUser_ValidUser_ReturnsOk()
        {
            // Arrange
            User user = new User()
            {
                Name = "Robert Test",
                Email = "rob@test.com",
                Address = "Robert 34",
                Phone = "+125468452385",
                UserType = UserType.NotAssigned,
                Money = 150.00m
            };
            var controller = new UsersController(Mock.Of<ILogger<UsersController>>(), Mock.Of<IUserRepository>());

            // Act
            var result = await controller.CreateUser(user) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("User created successfully.", result.Value);
        }

        [Fact]
        async void CreateUser_DuplicatedUser_ReturnsBadRequest()
        {
            // Arrange
            User user = new User()
            {
                Name = "Robert Test",
                Email = "rob@test.com",
                Address = "Robert 34",
                Phone = "+125468452385",
                UserType = UserType.NotAssigned,
                Money = 150.00m
            };

            Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.Add(It.IsAny<User>())).Throws(new DbUpdateException(null, new SqliteException("Duplicate entry", 19)));
            var controller = new UsersController(Mock.Of<ILogger<UsersController>>(), mockRepo.Object);

            // Act
            var result = await controller.CreateUser(user) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("This user is a duplicate and cannot be added.", result.Value);
        }
    }
}
