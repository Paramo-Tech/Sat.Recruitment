using FluentAssertions;
using Moq;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Models.User;
using Sat.Recruitment.Kernel.Features.Users.GetUsersQueryCommand;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.CommandHandlers
{
    public class GetUsersQueryCommandHandlerTests
    {
        private Mock<IUserService> _userServiceMock;

        public GetUsersQueryCommandHandlerTests()
        {
            _userServiceMock = new Mock<IUserService>();
        }

        [Fact]
        public async Task Handle_ShouldReturnUsers_WhenUsersExist()
        {
            // Arrange
            _userServiceMock.Setup(x => x.GetAsync())
                .Returns(Task.FromResult(GetListWithData().AsEnumerable()));
            var handler = new GetUsersQueryHandler(_userServiceMock.Object);
            var query = new GetUsersQueryRequest { };

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().NotBeNull();
        }

        private List<IUser> GetListWithData()
        {
            List<IUser> users = new List<IUser>();
            users.AddRange(new List<User>
            {
                new User { Id = "1", Name = "John", Address = "Doe", Email = "johndoe@example.com" },
                new User { Id = "2", Name = "Jane", Address = "Doe", Email = "janedoe@example.com" },
                new User { Id = "3", Name = "Bob", Address = "Smith", Email = "bobsmith@example.com" }
            });
            return users;
        }
    }
}
