using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Sat.Recruitment.Application.Users.Commnads;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Enums;
using Shouldly;
using static Sat.Recruitment.Application.IntegrationTests.Testing;

namespace Sat.Recruitment.Application.IntegrationTests.Users.Commands
{
    public class CreateUserCommandTests : BaseTestFixture
    {
        [Test]
        public async Task CreateUserCommandSuccess()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Address = "Address",
                Email = "Email@Email.com",
                Money = 200,
                Name = "Name",
                Phone = "1234567890",
                UserType = UserTypes.Premium
            };
            var datetimeUtc = DateTime.UtcNow;

            // Act
            (var response, Guid key) = await SendAsync(command);

            var user = await FindAsync<User>(key);

            // Assert
            response.ShouldNotBeNull();
            response.Succeeded.ShouldBeTrue();
            response.Errors.Count().ShouldBe(0);
            user.ShouldNotBeNull();
            user.Address.ShouldBe(command.Address);
            user.Email.ShouldBe(command.Email);
            user.Name.ShouldBe(command.Name);
            user.Phone.ShouldBe(command.Phone);
            user.UserType.ShouldBe(command.UserType);
            user.Money.ShouldBe(command.Money * 3);
            user.CreatedBy.ShouldBeNull();
            user.CreatedUtc.ShouldBeGreaterThanOrEqualTo(datetimeUtc);
            user.CreatedUtc.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
            user.LastModifiedBy.ShouldBeNull();
            user.LastModifiedUtc.ShouldBeGreaterThanOrEqualTo(datetimeUtc);
            user.LastModifiedUtc.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
        }

        [Test]
        public async Task CreateUserCommandFailure()
        {
            // Arrange
            await AddAsync(new User(name: "Address", email: "Email@Email.com", phone: "1234567890", address: "Address", userType: UserTypes.Premium, money: 200M, isNewUser: true));

            var command = new CreateUserCommand
            {
                Address = "Address",
                Email = "Email@Email.com",
                Money = 200,
                Name = "Name",
                Phone = "1234567890",
                UserType = UserTypes.Premium
            };
            var datetimeUtc = DateTime.UtcNow;

            // Act
            (var response, Guid key) = await SendAsync(command);

            var user = await FindAsync<User>(key);

            // Assert
            response.ShouldNotBeNull();
            response.Succeeded.ShouldBeFalse();
            response.Errors.Count().ShouldBe(1);
            response.Errors.ShouldAllBe(x => x == "The user is duplicated.");
            user.ShouldBeNull();
        }
    }
}
