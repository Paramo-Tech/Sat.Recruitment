using Application.Automapper.Profiles;
using Application.Commands;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Moq;
using Sat.Recruitment.Test.TestDoubles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Commands
{
    public class CreateUserCommandTests
    {
        Mock<IUserRepository> _userRepoMock;
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IMapper _mapper;

        public CreateUserCommandTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateUserCommand, User>();
            });

            _mapper = _configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Create_User_Command_Should_Call_Add_Method_In_Repository()
        {
            CreateUserCommand cmd = FakeCreateUserCommand.WithValidData;
            CreateUserCommandHandler cmdHandler = new CreateUserCommandHandler(_mapper, _userRepoMock.Object);
            await cmdHandler.Handle(cmd, new System.Threading.CancellationToken());
            _userRepoMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.AtLeastOnce());
        }

        [Fact]
        public async Task Create_User_Command_Should_Throw_Duplicated_UserException()
        {
            CreateUserCommand cmd = FakeCreateUserCommand.WithValidData;
            _userRepoMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(new User { Email = cmd.Email}));
            CreateUserCommandHandler cmdHandler = new CreateUserCommandHandler(_mapper, _userRepoMock.Object);
            await Assert.ThrowsAsync<DuplicateUserException>(() => cmdHandler.Handle(cmd, new System.Threading.CancellationToken()));
        }
    }
}
