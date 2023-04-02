using Application.Contracts;
using Application.Models;
using Moq;

using Xunit;

namespace Sat.Recruitment.Test.WevApi
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersController
    {
        private readonly Mock<IUserService> _userServiceMock;

        private readonly Api.Controllers.UsersController _userController;

        public UsersController()
        {

            _userServiceMock = new Mock<IUserService>();

            _userController = new Api.Controllers.UsersController(_userServiceMock.Object);
        }

        [Fact]
        public async void GivenACorrectUserValueWhenCreateUserThenIsCreated()
        {
            //Arrange                        
            var userCreationDto = new UserCreationDto
            {
                Name = "Reinaldo",
                Email = "reinaldo.aospino@gmail.com",
                Address = "Av. Jose G",
                Phone = "+349 1122358215",
                UserType = "Normal",
                Money = "124"
            };

            _userServiceMock.Setup(x => x.Create(userCreationDto)).ReturnsAsync(Result.Success("User Created"));


            //Act
            var result = await _userController.CreateUser(userCreationDto);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Message);
            Assert.Null(result.Errors);
        }

        [Fact]
        public async void GivenACorrectUserValueWhenCreateUserThenIsDpublicated()
        {
            //Arrange                        
            var userCreationDto = new UserCreationDto
            {
                Name = "Reinaldo",
                Email = "reinaldo.aospino@gmail.com",
                Address = "Av. Jose G",
                Phone = "+349 1122358215",
                UserType = "Normal",
                Money = "124"
            };

            _userServiceMock.Setup(x => x.Create(userCreationDto)).ReturnsAsync(Result.Failure("The user is duplicated"));

            //Act
            var result = await _userController.CreateUser(userCreationDto);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
            Assert.Null(result.Message);

        }
    }
}
