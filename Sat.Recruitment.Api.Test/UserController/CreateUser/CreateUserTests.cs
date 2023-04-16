using Moq;
using Sat.Recruitment.Business.Models;
using Sat.Recruitment.Test;
using Xunit;

namespace Sat.Recruitment.Api.Test.UserController.CreateUser
{
    public class CreateUserTests : UserControllerTests
    {
        [Fact]
        public void Will_Return_Error_Required_Properties_Missing()
        {
            #region Variables
            var user = new UserModel
            {
                Name = "John Doe",
                Email = "",
                Address = "",
                Money = 0,
                Phone = "",
                UserType = ""
            };
            var resultModel = new ResultModel()
            {
                Data= user,
                IsSuccess = false,
                Message = " The email is required The address is required The phone is required",

            };
            #endregion

            #region Setup
            MockService.Setup(s => s.CreateUser(user))
                .Returns(resultModel)
                .Verifiable();
            #endregion

            #region Call
            var result = Controller.CreateUser(user);
            #endregion

            #region Verify
            MockService.Verify(s => s.CreateUser(user), Times.Once);
            #endregion

            #region Assert
            Assert.IsType<ResultModel>(result);
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(" The email is required The address is required The phone is required", result.Message);
            #endregion
        }

        [Fact]
        public void Will_Return_User_Created()
        {
            #region Variables
            var user = new UserModel
            {
                Name = "John Doe",
                Email = "",
                Address = "",
                Money = 0,
                Phone = "",
                UserType = ""
            };
            var resultModel = new ResultModel()
            {
                Data = user,
                IsSuccess = true,
                Message = "User Created",

            };
            #endregion

            #region Setup
            MockService.Setup(s => s.CreateUser(user))
                .Returns(resultModel)
                .Verifiable();
            #endregion

            #region Call
            var result = Controller.CreateUser(user);
            #endregion

            #region Verify
            MockService.Verify(s => s.CreateUser(user), Times.Once);
            #endregion

            #region Assert
            Assert.IsType<ResultModel>(result);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Message);
            #endregion
        }

        [Fact]
        public void Will_Return_User_Already_Exists()
        {
            #region Variables
            var user = new UserModel
            {
                Name = "John Doe",
                Email = "email@email.com",
                Address = "avenue 433",
                Money = 12,
                Phone = "+4321212",
                UserType = "Normal"
            };
            var resultModel = new ResultModel()
            {
                Data = user,
                IsSuccess = false,
                Message = "The user is duplicated",

            };
            #endregion

            #region Setup
            MockService.Setup(s => s.CreateUser(user))
                .Returns(resultModel)
                .Verifiable();
            #endregion

            #region Call
            var result = Controller.CreateUser(user);
            #endregion

            #region Verify
            MockService.Verify(s => s.CreateUser(user), Times.Once);
            #endregion

            #region Assert
            Assert.IsType<ResultModel>(result);
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Message);
            #endregion
        }
    }
}
