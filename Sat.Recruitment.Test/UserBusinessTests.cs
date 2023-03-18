using Moq;
using Sat.Recruitment.Api.Business;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("UserBusinessTests", DisableParallelization = true)]
    public class UserBusinessTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        public UserBusinessTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task EmailShouldBeNormalizedAndGifShouldBeAppliedAndNewUserShouldBeReturned()
        {
            User user = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike.amigorena@gmail.com",
                Money = 500,
                Name = "Mike",
                UserType = "Normal"
            };

            User userAdded = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mikeamigorena@gmail.com",
                Money = 560,
                Name = "Mike",
                UserType = "Normal"
            };

            _userRepositoryMock.Setup(s => s.Insert(user)).ReturnsAsync(userAdded);
            UserBusiness userBusiness= new UserBusiness(_userRepositoryMock.Object);
            User newUser = await userBusiness.CreateUser(user);
            Assert.NotNull(newUser);
            Assert.Equal(userAdded.Name, newUser.Name);
            Assert.Equal(userAdded.Money, newUser.Money);
            Assert.Equal(userAdded.Email, newUser.Email);
        }

        [Fact]
        public void EmailShouldBeNormalized()
        {
            string normalizedEmail = UserBusiness.NormalizeEmail("lautaro.fernandez27@gmail.com");
            Assert.NotNull(normalizedEmail);
            Assert.Equal("lautarofernandez27@gmail.com", normalizedEmail);
        }
        [Fact]
        public void GifShouldBeCalculatedForNormalUserWithMoreThan100()
        {
            User user = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike.amigorena@gmail.com",
                Money = 200,
                Name = "Mike",
                UserType = "Normal"
            };
            double result = UserBusiness.AddUserGif(user);
            Assert.Equal(24, result);
        }
        [Fact]
        public void GifShouldBeCalculatedForNormalUserWithLessThan100()
        {
            User user = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike.amigorena@gmail.com",
                Money = 50,
                Name = "Mike",
                UserType = "Normal"
            };
            double result = UserBusiness.AddUserGif(user);
            Assert.Equal(4, result);
        }
        [Fact]
        public void GifShouldBeCalculatedForNormalUserWithLessThan10()
        {
            User user = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike.amigorena@gmail.com",
                Money = 5,
                Name = "Mike",
                UserType = "Normal"
            };
            double result = UserBusiness.AddUserGif(user);
            Assert.Equal(0, result);
        }
        [Fact]
        public void GifShouldBeCalculatedForPremiumUserWithLessThan100()
        {
            User user = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike.amigorena@gmail.com",
                Money = 5,
                Name = "Mike",
                UserType = "Premium"
            };
            double result = UserBusiness.AddUserGif(user);
            Assert.Equal(0, result);
        }
        [Fact]
        public void GifShouldBeCalculatedForPremiumUserWithMoreThan100()
        {
            User user = new User
            {
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                Email = "mike.amigorena@gmail.com",
                Money = 500,
                Name = "Mike",
                UserType = "Premium"
            };
            double result = UserBusiness.AddUserGif(user);
            Assert.Equal(1000, result);
        }
    }
}
