using Sat.Recruitment.DTOs.Models;
using Sat.Recruitment.DTOs.Requests;
using Sat.Recruitment.Services.Commands.Imp;
using System.Collections.Generic;
using Xunit;

namespace Sat.Recruitment.Test.Commands
{
    public class UserExistsCommandTests
    {
        [Fact]
        public void Execute_ReturnsTrue_WhenUserExistsByEmail()
        {
            var users = new List<User> { new User { Email = "alreadyexists@gmail.com" } };
            var request = new UserCreateRequest { Email = "alreadyexists@gmail.com" };
            var command = new UserExistsCommand();

            var result = command.Execute(users, request);

            Assert.True(result);
        }

        [Fact]
        public void Execute_ReturnsTrue_WhenUserExistsByPhone()
        {
            var users = new List<User> { new User { Phone = "1234567890" } };
            var request = new UserCreateRequest { Phone = "1234567890" };
            var command = new UserExistsCommand();

            var result = command.Execute(users, request);

            Assert.True(result);
        }

        [Fact]
        public void Execute_ReturnsTrue_WhenUserExistsByNameAndAddress()
        {
            var users = new List<User> { new User { Name = "Federico Marinangeli", Address = "Illia 50" } };
            var request = new UserCreateRequest { Name = "federico marinangeli", Address = "Illia 50" };
            var command = new UserExistsCommand();

            var result = command.Execute(users, request);

            Assert.True(result);
        }

        [Fact]
        public void Execute_ReturnsFalse_WhenUserDoesNotExist()
        {
            var users = new List<User>();
            var request = new UserCreateRequest { Email = "fedemarinangeli@gmail.com" };
            var command = new UserExistsCommand();

            var result = command.Execute(users, request);

            Assert.False(result);
        }
    }
}