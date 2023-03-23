using AutoMapper;
using Moq;
using Sat.Recruitment.Application.Commands.CreateUser;
using Sat.Recruitment.Domain.Entities.UserAggregate;

namespace Sat.Recruitment.ApplicationTest.Commons
{
    public static class MappingMock
    {
        public static IMapper Mapping()
        {
            var mock = new Mock<IMapper>();

            mock.Setup(x=> x.Map<User>(It.IsAny<CreateUserCommand>())).Returns(UserMock.DefaultUser);
            mock.Setup(x => x.Map<CreateUserCommandResponse>(It.IsAny<User>())).Returns(new CreateUserCommandResponse { Id = 1});

            return mock.Object;
        }
    }
}
