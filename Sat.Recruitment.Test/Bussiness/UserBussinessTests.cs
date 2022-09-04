using Sat.Recruitment.Bussiness;
using Sat.Recruitment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("UseBussinessTests", DisableParallelization = true)]
    public class UserBussinessTests
    {
        [Fact]
        public void GivenUserCreation_WhenUserHasSameNameAnotherAddress_ThenWontBeADuplicatedUserError()
        {
            var userToInsert = new User
            {
                Name = "Mike",
                Email = "mike2@gmail.com",
                Address = "Av. Juan G1",
                Phone = "+349 112323954215",
                UserType = "Normal",
                Money = 124
            };
            var userBussiness = new UserBussiness(new List<User> {
                new User{Name="Mike", Email="mike@gmail.com", Address="Av. Juan G", Phone="+349 1122354215", UserType="Normal", Money=124 },
            });

            userBussiness.CreateUser(userToInsert);
            var lastUsert = userBussiness.Users.LastOrDefault();

            Assert.Equal(userToInsert.Name, lastUsert.Name);
            Assert.Equal(userToInsert.Email, lastUsert.Email);
            Assert.Equal(userToInsert.Phone, lastUsert.Phone);
            Assert.Equal(userToInsert.Address, lastUsert.Address);
            Assert.Equal(userToInsert.UserType, lastUsert.UserType);
            
        }

        [Theory]
        [InlineData(null, "+349 11223542115", "Av. Juan G", "Mike", " The email is required")]//email 
        [InlineData("mike@gmail.com", null, "Av. Juan G", "Mike", " The phone is required")]//phone
        [InlineData("mike@gmail.com", "+349 11223542115", null, "Mike", " The address is required")]//address
        [InlineData("mike@gmail.com", "+349 11223542115", "Av. Juan G", null, "The name is required")]//name
        public void GivenUserCreation_WhenUserHasInvalidData_ThenItWillGetAnError(string email, string phone, string address, string name, string expectedErrorMessage)
        {
            var userBussiness = new UserBussiness(new List<User> {
                new User{Name="Mike", Email="mike@gmail.com", Address="Av. Juan G", Phone="+349 1122354215", UserType="Normal", Money=124 },
            });

            Action act = () =>
              userBussiness.CreateUser(new User { Name = name, Email = email, Address = address, Phone = phone, UserType = "Normal", Money = 124 });
            var exception = Assert.Throws<Exception>(act);


            Assert.NotNull(exception);
            Assert.Equal(expectedErrorMessage, exception.Message);
        }



        [Fact]
        public void GivenUserCreation_WhenUserEmailHasPlusChar_ThenUserEmailShouldBeStoredNormalize()
        {
            var userBussiness = new UserBussiness(new List<User>
            {
            });

            userBussiness.CreateUser(new User { Name = "Mike", Email = "mike+1@gmail.com", Address = "Av. Juan G", Phone = "+3491122354215", UserType = "Normal", Money = 124 });

            var lastUser = userBussiness.Users.LastOrDefault();
            var expectedEmail = "mike@gmail.com";

            Assert.Equal(expectedEmail, lastUser.Email);
        }
    }
}
