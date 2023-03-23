using Moq;
using Sat.Recruitment.Domain.Entities.UserAggregate;
using Sat.Recruitment.Domain.Interfaces.Data.Repositories;

namespace Sat.Recruitment.ApplicationTest.Commons
{
    public static class UserRepositoryMock
    {
        public static IUserRepository GetUserRepositoryMock()
        {
            var mock = new Mock<IUserRepository>();

            mock.Setup(x => x.AddUserAsync(It.IsAny<User>())).ReturnsAsync(1);
            mock.Setup(x=> x.IsDublicateUser("default", "default", "default", "default")).ReturnsAsync(false);
            mock.Setup(x=> x.GetUserByIdAsync(1)).ReturnsAsync(UserMock.DefaultUser);

            return mock.Object;
        }       
    }
}
