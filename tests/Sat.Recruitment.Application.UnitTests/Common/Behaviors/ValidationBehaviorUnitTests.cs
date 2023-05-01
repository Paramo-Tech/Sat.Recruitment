using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using Sat.Recruitment.Application.Common.Behaviors;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Common.Services.UserMoneyStrategies;
using Sat.Recruitment.Application.Users.Commands.CreateUser;
using Sat.Recruitment.Application.Users.EventHandlers;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.UnitTests.Common.Behaviors;

public class ValidationBehaviorUnitTests
{
    private readonly Mock<IApplicationDbContext> _dbContext;
    private readonly Mock<IUserGifService> _userGifService;

    public ValidationBehaviorUnitTests()
    {
        var users = new List<User>().AsQueryable();
        var usersDbSet = new Mock<DbSet<User>>();

        usersDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
        usersDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
        usersDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
        usersDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

        _dbContext = new Mock<IApplicationDbContext>();
        _dbContext.SetupGet(x => x.Users).Returns(usersDbSet.Object);

        _userGifService = new Mock<IUserGifService>();
        _userGifService.Setup(x => x.Calculate(It.IsAny<User>())).Returns(100);
    }

    [Fact]
    public async Task ShouldThrowValidationExceptionWhenHasValidationError()
    {
        var validationBehavior = new ValidationBehavior<CreateUserCommand, int>(
            new List<IValidator<CreateUserCommand>>()
            {
                new CreateUserCommandValidator(_dbContext.Object)
            }
        );
        var createUserCommand = new CreateUserCommand();
        var userCreatedEventHandler = new UserCreatedEventHandler(_dbContext.Object, _userGifService.Object);

        //act && assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            validationBehavior.Handle(createUserCommand, 
                () => userCreatedEventHandler.Handle(createUserCommand, CancellationToken.None), 
                CancellationToken.None)
        );
    }
}