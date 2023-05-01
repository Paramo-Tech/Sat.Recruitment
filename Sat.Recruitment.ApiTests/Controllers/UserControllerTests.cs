using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sat.Recruitment.Model.Shared;
using Sat.Recruitment.Model.User;
using Sat.Recruitment.Povider.IProvider;
using Assert = NUnit.Framework.Assert;

namespace Sat.Recruitment.Api.Controllers.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        private Mock<IUserProvider> _mockUserProvider;
        private Mock<ILogger<UserController>> _mockLogger;
        private UserController _userController;
        public UserControllerTests()
        {
            _mockUserProvider = new Mock<IUserProvider>();
            _mockLogger = new Mock<ILogger<UserController>>();
            _userController = new UserController(_mockUserProvider.Object, _mockLogger.Object);
        }

        [TestMethod()]
        public void CreateUser_True()
        {
            _mockUserProvider.Setup(_ => _.CreateUser(It.IsAny<UserModel>())).
                Returns(Task.FromResult(new ResponseModel()
                {
                    IsSuccess = true
                }));
            var result = _userController.CreateUser(new UserModel());
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }
        [TestMethod()]
        public void CreateUser_BadRequest()
        {
            _mockUserProvider.Setup(_ => _.CreateUser(It.IsAny<UserModel>())).
                Throws(new Exception());
            var result = _userController.CreateUser(new UserModel());
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
    }
}