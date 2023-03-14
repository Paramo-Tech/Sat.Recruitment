using Mapster;
using Moq;
using Sat.Recruitment.CommonTests.Builders;
using Sat.Recruitment.CommonTests.Builders.Dtos;
using Sat.Recruitment.CommonTests.TestsBases;
using Sat.Recruitment.Core.ResponsesExceptions;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Domain.IRepositories;
using Sat.Recruitment.Domain.Services;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.UnitTests.Services
{
    public class UserServiceTests : ServiceTestsBase
    {
        private readonly UserDto _dto;
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly IUserService _service;

        public UserServiceTests()
        {
            _dto = UserDtoBuilder.BuildInstance();
            _repositoryMock = Mocker.GetMock<IUserRepository>();
            _service = new UserService(_repositoryMock.Object);
        }

        #region Insert

        [Fact]
        public async Task Insert_With_ValidRequest_ReturnUserDto()
        {
            //Arrange
            User user = _dto.Adapt<User>();

            _repositoryMock.Setup(x => x.Insert(It.IsAny<User>())).ReturnsAsync(new User());

            //Act
            var result = await _service.Insert(_dto);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Insert_With_InvalidRequest_ReturnNull()
        {
            //Arrange
            _repositoryMock.Setup(x => x.Insert(It.IsAny<User>()));
            
            //Act
            var result = await _service.Insert(_dto);
            
            //Act and Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Insert_With_DuplicatedUserRequest_ReturnError()
        {           
            //Arrange
            var responseException = new BadRequestException()
            {
                ErrorMessage = SatRecruitmentConstants.ErrorMsgUserDuplicate
            };

            //Arrange
            _repositoryMock.Setup(x => x.Insert(It.IsAny<User>())).Throws(responseException);

            //Act and Assert
            var ex = await Assert.ThrowsAsync<BadRequestException>(() => _service.Insert(_dto));
            Assert.NotNull(ex);
        }

        #endregion

        #region GetUserType

        [Fact]
        public async Task GetUserType_With_ValidRequest_ReturnType()
        {
            //Arrange
            _repositoryMock.Setup(x => x.GetUserTypeByEmail(It.IsAny<string>())).ReturnsAsync(SharedValues.User_UserType);

            //Act
            var result = await _service.GetUserTypeByEmail(_dto.Email);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUserType_With_InvalidRequest_ReturnNull()
        {
            //Arrange
            _repositoryMock.Setup(x => x.GetUserTypeByEmail(It.IsAny<string>()));

            //Act
            var result = await _service.GetUserTypeByEmail(_dto.Email);

            //Act and Assert
            Assert.Null(result);
        }

        #endregion

        #region ValidateCredentials

        [Fact]
        public async Task ValidateCredentials_With_ValidRequest_ReturnSuccess()
        {
            //Arrange
            _repositoryMock.Setup(x => x.ValidateCredentials(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            //Act
            var result = await _service.ValidateCredentials(_dto.Email,_dto.Password);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateCredentials_With_InvalidRequest_ReturnNoSuccess()
        {
            //Arrange
            _repositoryMock.Setup(x => x.ValidateCredentials(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            //Act
            var result = await _service.ValidateCredentials(SharedValues.User_InvalidEmail, SharedValues.User_InvalidPassword);

            //Act and Assert
            Assert.False(result);
        }

        #endregion

    }
}



