using System;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Enums;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("UserMoneyTests")]
    public class UserMoneyTests
    {

        [Fact]
        public void MoneyCalculatedCorrectlyNormalUserMoreThan100()
        {
            var user = new User
            {
                UserType = UserType.Normal,
                Money = 500
            };
            var expectedMoney = 500 + (500 * 0.12m);

            var actualMoney = user.Money;

            Assert.Equal(expectedMoney, actualMoney);
        }

        [Fact]
        public void MoneyCalculatedCorrectlyNormalUserLessThan10()
        {
            var user = new User
            {
                UserType = UserType.Normal,
                Money = 5
            };
            var expectedMoney = 5;

            var actualMoney = user.Money;

            Assert.Equal(expectedMoney, actualMoney);
        }

        [Fact]
        public void MoneyCalculatedCorrectlyNormalUserLessThan100()
        {
            var user = new User
            {
                UserType = UserType.Normal,
                Money = 50
            };
            var expectedMoney = 50 + (50 * 0.8m);

            var actualMoney = user.Money;

            Assert.Equal(expectedMoney, actualMoney);
        }



        [Fact]
        public void MoneyCalculatedCorrectlySuperUser()
        {
            var user = new User
            {
                UserType = UserType.SuperUser,
                Money = 150
            };
            var expectedMoney = 150 + (150 * 0.2m);

            var actualMoney = user.Money;

            Assert.Equal(expectedMoney, actualMoney);
        }

        [Fact]
        public void MoneyCalculatedCorrectlyPremiumUser()
        {
            var user = new User
            {
                UserType = UserType.Premium,
                Money = 200
            };
            var expectedMoney = 200 + (200 * 2);

            var actualMoney = user.Money;

            Assert.Equal(expectedMoney, actualMoney);
        }
    }

}
