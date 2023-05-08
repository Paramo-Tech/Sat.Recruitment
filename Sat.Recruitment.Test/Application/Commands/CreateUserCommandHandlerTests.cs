using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Application.Command;
using Sat.Recruitment.Application.Dto.User;
using Sat.Recruitment.Application.Exceptions;
using Sat.Recruitment.Application.Services.GifCalculator.Factory;
using Sat.Recruitment.Application.Services.GifCalculator.Strategy;
using Sat.Recruitment.Domain.Enum;
using Sat.Recruitment.Domain.Model;
using Sat.Recruitment.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Application.Commands
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IUserGifCalculatorFactory> _gifCalculatorFactory;
        private readonly Mock<IUserGifCalculator> _gifCalculator;
        private readonly Mock<ILogger<CreateUserCommandHandler>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly CreateUserCommandValidator _validator;

        public CreateUserCommandHandlerTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _gifCalculatorFactory = new Mock<IUserGifCalculatorFactory>();
            _gifCalculator = new Mock<IUserGifCalculator>();
            _logger = new Mock<ILogger<CreateUserCommandHandler>>();
            _mapper = new Mock<IMapper>();
            _validator = new CreateUserCommandValidator();
        }

        [Fact]
        public async Task HandleCommand_WithValidCommand_ReturnsUser()
        {
            // Arrange.
            var address = "Fake St 123";
            var email = "fake@fake.com";
            var money = 1M;
            var name = "John Doe";
            var phone = "+5493446371435";
            var type = UserType.Normal;

            var repositoryUser = new User(name, email, phone, address, type, money);
            var expected = new UserDto()
            {
                Address = address,
                Email = email,
                Money = money,
                UserType = type,
                Name = name,
                Phone = phone
            };
            var request = new CreateUserCommand()
            {
                Address = address,
                Email = email,
                Money = money,
                Name = name,
                Phone = phone,
                UserType = type
            };

            _gifCalculator.Setup(x => x.Calculate(It.IsAny<decimal>())).Returns(10);
            _gifCalculatorFactory.Setup(x => x.CreateCalculator(It.IsAny<UserType>())).Returns(_gifCalculator.Object);
            _userRepository.Setup(x => x.GetAll()).Returns(new List<User>());
            _userRepository.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(repositoryUser);
            _mapper.Setup(x => x.Map<UserDto>(It.IsAny<User>())).Returns(expected);

            var handler = new CreateUserCommandHandler(_logger.Object, _userRepository.Object, _validator, _gifCalculatorFactory.Object, _mapper.Object);

            // Act.
            var result = await handler.Handle(request);

            // Assert.
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("jdoe@fake.com", "Jane Doe", "Evergreen Terrace 232","+5493446371435")]
        [InlineData("fake@fake.com", "Jane Doe", "Evergreen Terrace 232","+5493446371436")]
        [InlineData("jdoe@fake.com", "John Doe", "Fake St 123", "+5493446371436")]
        public async Task HandleCommand_WithRepeatedUser_ThrowsException(string newEmail, string newName, string newAddress, string newPhone)
        {
            // Arrange.
            var address = "Fake St 123";
            var email = "fake@fake.com";            
            var money = 1M;
            var name = "John Doe";
            var phone = "+5493446371435";
            var type = UserType.Normal;
           
            var user = new User(name, email, phone, address, type, money);            

            var request = new CreateUserCommand()
            {
                Address = newAddress,
                Email = newEmail,
                Money = money,
                Name = newName,
                Phone = newPhone,
                UserType = type
            };

            _gifCalculator.Setup(x => x.Calculate(It.IsAny<decimal>())).Returns(10);
            _gifCalculatorFactory.Setup(x => x.CreateCalculator(It.IsAny<UserType>())).Returns(_gifCalculator.Object);
            _userRepository.Setup(x => x.GetAll()).Returns(new List<User>() { user } );
            _userRepository.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(user);            

            var handler = new CreateUserCommandHandler(_logger.Object, _userRepository.Object, _validator, _gifCalculatorFactory.Object, _mapper.Object);

            // Act.
            async Task handle() => await handler.Handle(request);

            // Assert.
            await Assert.ThrowsAsync<RepeatedUserException>(handle);
        }
    }
}
