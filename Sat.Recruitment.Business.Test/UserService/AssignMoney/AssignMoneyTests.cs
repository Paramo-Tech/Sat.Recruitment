using Sat.Recruitment.Business.Models;
using Sat.Recruitment.Business.Tests.UserServiceTests;
using Xunit;

namespace Sat.Recruitment.Business.Test.UserService.AssignMoney
{
    public class AssignMoneyTests : UserServiceTests
    {
        [Fact]
        public void Will_Return_Normal_Greather_Than_100()
        {
            #region Variables
            var user = new UserModel
            {
                Name = "John Doe",
                Email = "",
                Address = "",
                Money = 1000,
                Phone = "",
                UserType = "Normal"
            };
            #endregion

            #region Setup
            #endregion

            #region Call
            var result = Service.AssignMoney(user);
            #endregion

            #region Verify
            #endregion

            #region Assert
            Assert.IsType<decimal>(result);            
            Assert.Equal(1120, result);
            #endregion
        }

        [Fact]
        public void Will_Return_Normal_Less_Than_100_But_Greather_Than_10()
        {
            #region Variables
            var user = new UserModel
            {
                Name = "John Doe",
                Email = "",
                Address = "",
                Money = 60,
                Phone = "",
                UserType = "Normal"
            };
            #endregion

            #region Setup
            #endregion

            #region Call
            var result = Service.AssignMoney(user);
            #endregion

            #region Verify
            #endregion

            #region Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(108, result);
            #endregion
        }

        [Fact]
        public void Will_Return_SuperUser_Greather_Than_100()
        {
            #region Variables
            var user = new UserModel
            {
                Name = "John Doe",
                Email = "",
                Address = "",
                Money = 1000,
                Phone = "",
                UserType = "SuperUser"
            };
            #endregion

            #region Setup
            #endregion

            #region Call
            var result = Service.AssignMoney(user);
            #endregion

            #region Verify
            #endregion

            #region Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(1200, result);
            #endregion
        }

        [Fact]
        public void Will_Return_Premiun_Greather_Than_100()
        {
            #region Variables
            var user = new UserModel
            {
                Name = "John Doe",
                Email = "",
                Address = "",
                Money = 1000,
                Phone = "",
                UserType = "Premiun"
            };
            #endregion

            #region Setup
            #endregion

            #region Call
            var result = Service.AssignMoney(user);
            #endregion

            #region Verify
            #endregion

            #region Assert
            Assert.IsType<decimal>(result);
            Assert.Equal(1000, result);
            #endregion
        }
    }
}
