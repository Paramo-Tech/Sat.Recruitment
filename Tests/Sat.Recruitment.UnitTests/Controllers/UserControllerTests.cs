using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.CommonTests.Builders.Dtos;
using Sat.Recruitment.CommonTests.TestsBases;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Web.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.UnitTests.Controllers
{
    public class UserControllerTests : ControllerTestsBase<UsersController>
    {

        private UserDto _dto;
        private readonly Mock<IUserService> _userServiceMock;

        public UserControllerTests()
        {
            _dto = UserDtoBuilder.BuildInstance();
            _userServiceMock = Mocker.GetMock<IUserService>();
        }

        #region CreateUser

        [Fact]
        public async Task CreateUser_WithInValidResult_ReturnInternalServerError()
        {
            //Arrange
            _userServiceMock.Setup(x => x.Insert(It.IsAny<UserDto>()));

            //Act
            var result = await Controller.CreateUser(_dto);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);

            var objectResult = Assert.IsAssignableFrom<StatusCodeResult>(result.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateUser_WithValidRequest_ReturnOk()
        {          
            //Arrange
            _userServiceMock.Setup(x => x.Insert(It.IsAny<UserDto>())).ReturnsAsync(_dto);

            //Act
            var result = await Controller.CreateUser(_dto);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);

            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateUser_Call_Insert_FromService()
        {
            //Arrange
            _userServiceMock.Setup(x => x.Insert(It.IsAny<UserDto>())).ReturnsAsync(_dto);

            // Act
            await Controller.CreateUser(_dto);

            // Assert
            _userServiceMock.Verify(x => x.Insert(_dto), Times.Once);
        }


        #endregion

        #region GetUserTypeByEmail

        [Fact]
        public async Task GetUserTypeByEmail_WithInValidRequest_ReturnBadRequest()
        {
            //Arrange

            //Act
            var result = await Controller.GetUserTypeByEmail(string.Empty);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);

            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);

            Assert.Equal(SatRecruitmentConstants.ErrorMsgEmailRequired, objectResult.Value.ToString());

        }

        [Fact]
        public async Task GetUserTypeByEmail_ReturnsOkAndDto()
        {
            //Arrange
            _userServiceMock.Setup(x => x.GetUserTypeByEmail(It.IsAny<string>())).ReturnsAsync(_dto.UserType);

            //Act
            var result = await Controller.GetUserTypeByEmail(_dto.Email);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);

            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }
        
        [Fact]
        public async Task GetUserTypeByEmail_WithInValidResult_ReturnInternalServerError()
        {
            //Arrange
            _userServiceMock.Setup(x => x.GetUserTypeByEmail(It.IsAny<string>()));

            //Act
            var result = await Controller.GetUserTypeByEmail(_dto.Email);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Result);

            var objectResult = Assert.IsAssignableFrom<StatusCodeResult>(result.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetUserTypeByEmail_Call_Insert_FromService()
        {
            //Arrange
            _userServiceMock.Setup(x => x.GetUserTypeByEmail(It.IsAny<string>())).ReturnsAsync(_dto.UserType);

            // Act
            await Controller.GetUserTypeByEmail(_dto.Email);

            // Assert
            _userServiceMock.Verify(x => x.GetUserTypeByEmail(_dto.Email), Times.Once);
        }


        #endregion


    }
}