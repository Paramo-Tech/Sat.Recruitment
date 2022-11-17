using Sat.Recruitment.Api.Entitys;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        readonly IUserBussiness userBussiness;

        public UnitTest1()
        {
            userBussiness = new UserBusiness();
        }

        [Fact]
        public void Test1()
        {
            var newUser = new User
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = decimal.Parse("124")
            };

            var result = userBussiness.AddUser(newUser).Result;

            Assert.True(result.IsSuccess);
            Assert.Equal("User Created", result.Errors);

        }

        [Fact]
        public void Test2()
        {
            var newUser = new User
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = decimal.Parse("124")
            };

            var result = userBussiness.AddUser(newUser).Result;
            Assert.False(result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);

        }
    }
}
