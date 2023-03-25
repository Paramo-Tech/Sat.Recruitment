using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Application.Extensions;
using Sat.Recruitment.DataAccess.OnMemory.Extensions;
using Sat.Recruitment.Domain.Contract.Users;
using Sat.Recruitment.Domain.Models.Enum;
using Sat.Recruitment.Domain.Models.Users;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private ServiceProvider _serviceProvider;
        private readonly IUserService _userService;
        private readonly IValidator<User> _validator;

        public UnitTest1() 
        {
            var services = new ServiceCollection();
            services.LoadOnMemoryDataBase();
            services.ConfigureDEMOServices();
            _serviceProvider = services.BuildServiceProvider();
            _userService = _serviceProvider.GetService<IUserService>();
            _validator = _serviceProvider.GetService<IValidator<User>>();
        }

        #region Integration tests

        /*
            Load the initial on memory database. Check inserts and duplicates with the given data
         */

        [Theory]
        [InlineData("Gerardo", "Gerardo@gmail.com", "Los Incas", "+349 123456789", "Normal", 124)]
        public async Task CallControllerToCreateNewUserWithNoDuplicatedDataShouldBeInserted(string name, string email, string address, string phone, string userType, decimal money)
        {
            var log = new Mock<Microsoft.Extensions.Logging.ILogger<UsersController>>();
            var controller = new UsersController(_userService, log.Object);
            var result = await controller.CreateUserAsync(new User(name, email, address, phone, userType, money));

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Theory]
        [InlineData("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124)]
        public async Task NewUserWithNoDuplicatedDataShouldBeInserted(string name, string email, string address, string phone, string userType, decimal money)
        {
            var result = await _userService.AddItemAsync(new User(name, email, address, phone, userType, money));
            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Theory]
        [InlineData("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124)]
        [InlineData("Franco", "Franco.Perez@gmail.com", "+534645213542", "Alvear y Colombres", "Premium", 112234)]
        [InlineData("Juan", "Juan@marmol.com", "+5491154762312", "Peru 2464", "Normal", 1234)]
        public async Task NewUserWithDuplicatedDataShouldFail(string name, string email, string address, string phone, string userType, decimal money)
        {            
            var result = await _userService.AddItemAsync(new User(name, email, address, phone, userType, money));

            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Theory]
        [InlineData("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124)]
        [InlineData("Franco", "Franco.Perez@gmail.com", "+534645213542", "Alvear y Colombres", "Premium", 112234)]
        [InlineData("Juan", "Juan@marmol.com", "+5491154762312", "Peru 2464", "Normal", 1234)]
        public async Task CallControllerToCreateNewUserWithDuplicatedDataShouldFail(string name, string email, string address, string phone, string userType, decimal money)
        {
            var log = new Mock<Microsoft.Extensions.Logging.ILogger<UsersController>>();
            var controller = new UsersController(_userService, log.Object);
            var result = await controller.CreateUserAsync(new User(name, email, address, phone, userType, money));

            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }

        #endregion

        #region Unit Test

        [Theory]
        [InlineData("", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124)]
        [InlineData("Agustina", "@Agustinagmail.", "Av. Juan G", "+349 1122354215", "Normal", 124)]
        [InlineData("Agustina", "", "Av. Juan G", "+349 1122354215", "Normal", 124)]
        [InlineData("Agustina", "valid@mail.com", "", "+349 1122354215", "Normal", 124)]
        [InlineData("Agustina", "valid@mail.com", "Av. Juan G", "", "Normal", 124)]
        public void CreateUserShouldFail(string name, string email, string address, string phone, string userType, decimal money)
        {
            var user = new User(name, email, address, phone, userType, money);
            Assert.False(_validator.Validate(user).IsValid);
        }

        [Theory]
        [InlineData(EUserType.Normal, 124, 138.88)]
        [InlineData(EUserType.Normal, 99, 178.2)]
        [InlineData(EUserType.SuperUser, 150, 180)]
        [InlineData(EUserType.Premium, 50, 50)]
        [InlineData(EUserType.Premium, 200, 600)]
        public void ProfitsShouldBeCorrect(EUserType userType, decimal money, decimal expectedProfit)
        {
            Assert.Equal(_userService.GetUserProfit(userType, money), expectedProfit);
        }

        [Theory]
        [InlineData(EUserType.Normal, 124, 138.8)]
        [InlineData(EUserType.Normal, 99, 99)]
        [InlineData(EUserType.SuperUser, 150, 80)]
        [InlineData(EUserType.Premium, 50, 150)]
        [InlineData(EUserType.Premium, 200, 400)]
        public void ProfitsShouldBeWrong(EUserType userType, decimal money, decimal expectedProfit)
        {
            Assert.NotEqual(_userService.GetUserProfit(userType, money), expectedProfit);
        }

        #endregion
    }
}
