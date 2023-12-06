using NUnit.Framework;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Repository;
using Sat.Recruitment.Api.Services;

namespace Sat.Recruitment.Test
{
    public class UserControllerTest
    {
        private IUserRepository _userRepository;
        private IUserService _userService;
        private UsersController _usersController;

        [SetUp]
        public void SetUp()
        {
            _userRepository = new UserRepository();
            _userService = new UserService(_userRepository);
            _usersController = new UsersController(_userService);
        }

        [Test]
        public void CreateUser_ValidUser_ReturnsOk()
        {
            var user = new User
            {
                Name = "Test",
                Address = "Address",
                Email = "test@gmail.com",
                Money= 345,
                Phone= "23456789",
                UserType= "Normal"
            };

            dynamic result = _usersController.CreateUser(user);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, "User Created");
        }

        [Test]
        public void CreateUser_InvalidUserType_ReturnsBadRequest()
        {
            var user = new User
            {
                Name = "Test",
                Address = "Address",
                Email = "test@gmail.com",
                Money = 345,
                Phone = "23456789",
                UserType = "Normals"
            };

            dynamic result = _usersController.CreateUser(user);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 400);
            Assert.AreEqual(result.Value, "Invalid userType");
        }

        [Test]
        public void CreateUser_UserDuplicated_ReturnsBadRequest()
        {
            var user = new User
            {
                Name = "Juan",
                Address = "Address",
                Email = "Juan@marmol.com",
                Money = 345,
                Phone = "+5491154762312",
                UserType = "Normal"
            };

            dynamic result = _usersController.CreateUser(user);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 400);
            Assert.AreEqual(result.Value, "The user is duplicated");
        }
    }
}
