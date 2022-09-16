using System;
using System.Collections.Generic;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.BusinessLogic;
using Sat.Recruitment.Api.BusinessLogic.Exceptions;
using Sat.Recruitment.Api.BusinessLogic.Model;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Data;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        Mock<IDataService> _dataServiceMock = new Mock<IDataService>();
        IApplicationBL _applicationBL;

        public UnitTest1()
        {
            _dataServiceMock.Setup(x => x.GetUsers()).Returns(GetUsers());

            _applicationBL = new ApplicationBL(_dataServiceMock.Object);

        }

        private List<User> GetUsers()
        {
            var _users = new List<User>();
            _users.Add(new User() { Name = "Oscar Wilde", Email = "owilde@gmail.com", Phone = "0101", Address = "Dublin", Money = 10 });
            _users.Add(new User() { Name = "Julio Cortazar", Email = "jcortazar@gmail.com", Phone = "0101", Address = "Bruselas", Money = 10 });

            return _users;
        }

        [Theory]
        [InlineData("Oscar Wilde", "test@t.com", "1231", "Dublin")]
        [InlineData("JC", "jcortazar@gmail.com", "1234", "test")]
        [InlineData("test", "test@gmail.com", "0101", "test")]
        public void ShouldThrowEDuplicateUserException(string name, string email, string phone, string addresss)
        {
            var user = new User() { Name = name, Email = email, Phone = phone, Address = addresss };
            Action act = () => _applicationBL.SaveUser(user);
            Assert.Throws<EDuplicatedUserException>(act);
        }

        [Theory]
        [InlineData("Oscar F. Wilde", "test@t.com", "1231", "Dublin")]
        [InlineData("JC", "jcortazar2@gmail.com", "1234", "test")]
        [InlineData("test", "test@gmail.com", "01012", "test")]
        public void ShouldNotThrowEDuplicateUserException(string name, string email, string phone, string addresss)
        {
            var user = new User() { Name = name, Email = email, Phone = phone, Address = addresss };
            Action act = () => _applicationBL.SaveUser(user);
            var exception = Record.Exception(act);
            Assert.Null(exception);
        }

        [Theory]
        [InlineData("Normal", 101, .12)]
        [InlineData("Normal", 120, .12)]
        [InlineData("Normal", 1200,.12)]
        [InlineData("Normal", 11, .80)]
        [InlineData("Normal", 50, .80)]
        [InlineData("Normal", 99, .80)]
        [InlineData("Normal", 0, 0)]
        [InlineData("Normal", 1, 0)]
        [InlineData("Normal", 9, 0)]
        [InlineData("SuperUser", 101, .20)]
        [InlineData("Superuser", 200, .20)]
        [InlineData("SuperUser", 1500, .20)]
        [InlineData("SuperUser", 20, 0)]
        [InlineData("SuperUser", 41, 0)]
        [InlineData("SuperUser", 99, 0)]
        [InlineData("SuperUser", 0, 0)]
        [InlineData("SuperUser", 1, 0)]
        [InlineData("SuperUser", 9, 0)]
        [InlineData("Premium", 101,  2)]
        [InlineData("Premium", 200,  2)]
        [InlineData("Premium", 1500, 2)]
        [InlineData("Premium", 20, 0)]
        [InlineData("Premium", 41, 0)]
        [InlineData("Premium", 99, 0)]
        [InlineData("Premium", 0, 0)]
        [InlineData("Premium", 1, 0)]
        [InlineData("Premium", 9, 0)]
        public void ShouldGiftBeCorrect(string userType, decimal money, decimal percent)
        {
            var user = new User() { Name = "n1", Email = "e1", Phone = "01", Address = "a1", UserType = userType, Money = money };
            _applicationBL.SaveUser(user);
            Assert.Equal(money + money * percent , user.Money);
        }



    }
}
