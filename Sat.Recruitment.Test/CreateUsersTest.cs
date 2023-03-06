using System.Threading.Tasks;
using Xunit;
using Sat.Recruitment.Application.Users.Commands.CreateUser;
using FluentValidation.TestHelper;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class CreateUsersTest
    {
        [Fact]
        public async Task SaveAsync_User_Success()
        {
            //Arrange
            var helper = new TestHelper();
            var dbContext = helper.GetInMemoryDb();
            var handle = new CreateUserCommandHandle(dbContext);
            var command = new CreateUserCommand()
            {
                Name = "Diego",
                Email = "diego@gmail.com",
                Address = "Street 4",
                Phone = "3005552288",
                UserType = 0,
                Money = 101
            };
            //Act
            var result = await handle.Handle(command, cancellationToken: default);            
            //Assert
            Assert.Equal(1,result);
        }

        [Fact]
        public async Task Should_have_error_when_data_is_invalid()
        {
            //Arrange
            var helper = new TestHelper();
            var dbContext = helper.GetInMemoryDb();            
            CreateUserCommandValidator validator = new(dbContext);
            
            var command = new CreateUserCommand()
            {
                Name = "",
                Email = "diego",
                Address = "",
                Phone = "",
                UserType = 0,
                Money = 101
            };
            //Act
            var result = await validator.TestValidateAsync(command);
            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Name);
            result.ShouldHaveValidationErrorFor(command => command.Email);
            result.ShouldHaveValidationErrorFor(command => command.Address);
            result.ShouldHaveValidationErrorFor(command => command.Phone);
        }        
    }
}
