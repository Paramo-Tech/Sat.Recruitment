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
        public void AddUser()
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
        public void AddUserDuplicated()
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

        [Fact]
        public void CalGiftSuperUser()
        {
            GiftContext giftContext = new GiftContext();
            decimal result = giftContext.GetPercentaje("SuperUser", decimal.Parse("124"));
            Assert.Equal(result, (decimal)24.8);

        }

        [Fact]
        public void CalGiftPremiumUser()
        {
            GiftContext giftContext = new GiftContext();
            decimal result = giftContext.GetPercentaje("Premium", decimal.Parse("124"));
            Assert.Equal(result, (decimal)248);

        }

        [Fact]
        public void CalGiftNormalUserHigh()
        {
            GiftContext giftContext = new GiftContext();
            decimal result = giftContext.GetPercentaje("Normal", decimal.Parse("124"));
            Assert.Equal(result, (decimal)14.88);

        }

        [Fact]
        public void CalGiftNormalUserlow()
        {
            GiftContext giftContext = new GiftContext();
            decimal result = giftContext.GetPercentaje("Normal", decimal.Parse("24"));
            Assert.Equal(result, (decimal)19.2);

        }

    }
}
