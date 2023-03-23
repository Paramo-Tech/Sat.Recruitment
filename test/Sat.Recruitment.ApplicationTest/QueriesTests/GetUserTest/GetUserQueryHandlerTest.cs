using Moq;
using Sat.Recruitment.Application.Exceptions;
using Sat.Recruitment.Application.Queries.GetUser;
using Sat.Recruitment.ApplicationTest.Commons;
using Sat.Recruitment.Domain.Interfaces.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.ApplicationTest.QueriesTests.GetUserTest
{
    public class GetUserQueryHandlerTest
    {
        [Fact]
        public async void get_user_successful()
        {
            var repository = UserRepositoryMock.GetUserRepositoryMock();

            var log = LoggerMock.LogGetUserQueryHandler();

            var query = new GetUserQueryHandler(repository, log);

            var response = await query.Handle(CommandMock.SomeUserId, CancellationToken.None);

            Assert.True(response.Id == 1);
            Assert.True(response.Name == "angel cruz");
            Assert.True(response.UserType == "Normal");

        }

        [Fact]
        public async void get_user_notfound()
        {
            var repository = new Mock<IUserRepository>();

            var log = LoggerMock.LogGetUserQueryHandler();

            var query = new GetUserQueryHandler(repository.Object, log);

            await Assert.ThrowsAsync<NotFoundException>(() => query.Handle(CommandMock.SomeUserId, CancellationToken.None));
        }
    }
}
