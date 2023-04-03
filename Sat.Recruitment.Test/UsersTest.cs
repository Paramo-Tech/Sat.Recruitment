using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UsersTest
    {
        [Fact]
        void ProcessNewUser_NormalUserOver100_Gets12PercentReward()
        {
            // Arrange
            decimal money = 150m;
            decimal percentage = 0.12m;
            var user = new User()
            {
                Name = "Robert Test",
                Email = "rob@test.com",
                Address = "Robert 34",
                Phone = "+125468452385",
                UserType = UserType.Normal,
                Money = money
            };
            var expected = money + money * percentage;

            // Act
            user.ProcessNewUser();

            // Assert
            Assert.Equal(expected, user.Money);
        }

        [Fact]
        void ProcessNewUser_NormalUserUnder100Over10_Gets80PercentReward()
        {
            // Arrange
            decimal money = 90;
            decimal percentage = 0.8m;
            var user = new User()
            {
                Name = "Robert Test",
                Email = "rob@test.com",
                Address = "Robert 34",
                Phone = "+125468452385",
                UserType = UserType.Normal,
                Money = money
            };
            var expected = money + money * percentage;

            // Act
            user.ProcessNewUser();

            // Assert
            Assert.Equal(expected, user.Money);
        }

        [Fact]
        void ProcessNewUser_NormalUserUnder10_GetsNoReward()
        {
            // Arrange
            var money = 9;
            var user = new User()
            {
                Name = "Robert Test",
                Email = "rob@test.com",
                Address = "Robert 34",
                Phone = "+125468452385",
                UserType = UserType.Normal,
                Money = money
            };

            // Act
            user.ProcessNewUser();

            // Assert
            Assert.Equal(money, user.Money);
        }

        [Fact]
        void ProcessNewUser_SuperUserOver100_Gets20PercentReward()
        {
            // Arrange
            var money = 150;
            decimal percentage = 0.2m;
            var user = new User()
            {
                Name = "Robert Test",
                Email = "rob@test.com",
                Address = "Robert 34",
                Phone = "+125468452385",
                UserType = UserType.SuperUser,
                Money = money
            };
            var expected = money + money * percentage;

            // Act
            user.ProcessNewUser();

            // Assert
            Assert.Equal(expected, user.Money);
        }

        [Fact]
        void ProcessNewUser_SuperUserUnder100_GetsNoReward()
        {
            // Arrange
            var money = 50;
            var user = new User()
            {
                Name = "Robert Test",
                Email = "rob@test.com",
                Address = "Robert 34",
                Phone = "+125468452385",
                UserType = UserType.SuperUser,
                Money = money
            };

            // Act
            user.ProcessNewUser();

            // Assert
            Assert.Equal(money, user.Money);
        }

        [Fact]
        void ProcessNewUser_PremiumUserOver100_Gets200PercentReward()
        {
            // Arrange
            var money = 150;
            decimal percentage = 2;
            var user = new User()
            {
                Name = "Robert Test",
                Email = "rob@test.com",
                Address = "Robert 34",
                Phone = "+125468452385",
                UserType = UserType.Premium,
                Money = money
            };
            decimal expected = money + money * percentage;

            // Act
            user.ProcessNewUser();

            // Assert
            Assert.Equal(expected, user.Money);
        }

        [Fact]
        void ProcessNewUser_PremiumUserUnder100_GetsNoReward()
        {
            // Arrange
            var money = 50;
            var user = new User()
            {
                Name = "Robert Test",
                Email = "rob@test.com",
                Address = "Robert 34",
                Phone = "+125468452385",
                UserType = UserType.Premium,
                Money = money
            };

            // Act
            user.ProcessNewUser();

            // Assert
            Assert.Equal(money, user.Money);
        }

        [Fact]
        void ProcessNewUser_NormalizeEmail_RemovesDots()
        {
            // Arrange
            var user = new User()
            {
                Name = "Robert Test",
                Email = "bob.odenkirk@test.com",
                Address = "Robert 34",
                Phone = "+125468452385",
                UserType = UserType.Premium,
                Money = 900
            };

            // Act
            user.ProcessNewUser();

            // Assert
            Assert.False(user.Email.Split('@')[0].Contains('.'));
        }
    }
}
