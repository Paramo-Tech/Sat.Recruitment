using Moq;
using Sat.Recruitment.Business.Models;
using Sat.Recruitment.Business.Tests.UserServiceTests;
using System.IO;
using System.Text;
using TypeMock.ArrangeActAssert;
using Xunit;

namespace Sat.Recruitment.Business.Test.UserService.CreateUser
{
    public class CreateUserTests : UserServiceTests
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
            #endregion

            #region Setup
            #endregion

            #region Call
            var result = Service.CreateUser(user);
            #endregion

            #region Verify
            #endregion

            #region Assert
            Assert.IsType<ResultModel>(result);
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(" The email is required The address is required The phone is required", result.Message);
            #endregion
        }

    }
}
