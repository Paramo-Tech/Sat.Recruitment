using System.IO;
using Moq;
using Sat.Recruitment.Api.Business;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models.Dtos;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserTest
    {
        private UsersController _controller;
        private IUserBusiness _userBusiness;
        private Mock<IFileHandlerService> _fileHandlerService;

        public UserTest()
        {
            _fileHandlerService = new Mock<IFileHandlerService>();
            _userBusiness = new UserBusiness(_fileHandlerService.Object);
            _controller = new UsersController(_userBusiness);

            var fileHandlerMock = new FileHandlerService();

            _fileHandlerService.Setup(x => x.GetTxtFileLines(Directory.GetCurrentDirectory() + "/Files/Users.txt"))
            .Returns(fileHandlerMock.GetTxtFileLines(Directory.GetCurrentDirectory() + "/Files/Users.txt")).Verifiable();
        }


        [Fact]
        public async void Create_Existing_User()
        {
            var user = GetGenericUser();
            string existingUserEmail = "Juan@marmol.com";

            user.Email = existingUserEmail;

            var result = await _controller.CreateUserAsync(user);

            Assert.False(result.IsSuccess);
            Assert.Equal("User already exists.", result.Message);
        }

        [Fact]
        public async void Create_Non_Existing_User()
        {
            var user = GetGenericUser();

            var result = await _controller.CreateUserAsync(user);

            Assert.True(result.IsSuccess);
            Assert.Equal("User created.", result.Message);
        }


        private UserDto GetGenericUser()
        {
            return new UserDto()
            {
                Name = "Pedro Gomez",
                Email = "pedro@marmol.com",
                Address = "Av. Sarmiento 34",
                Phone = "113454365",
                UserType = "Premium",
                Money = 3455
            };
        }


    }
}
