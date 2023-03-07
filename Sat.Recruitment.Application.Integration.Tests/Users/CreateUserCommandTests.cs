using FluentAssertions;
using NUnit.Framework;
using Sat.Recruitment.Application.Common.Exceptions;
using Sat.Recruitment.Application.Users.Commands;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Integration.Test.Users
{
    using static Testing;

    public class CreateUserCommandTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldRequireMinimumFields()
        {
            var command = new CreateUserCommand();
            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<CustomValidationException>();
        }

        [TestCase("Normal", 10, 10)]
        [TestCase("Normal", 50, 90)]
        [TestCase("Normal", 101, 113.12)]
        [TestCase("Normal", 100, 100)]

        [TestCase("SuperUser", 100, 100)]
        [TestCase("SuperUser", 101, 121.2)]        

        [TestCase("Premium", 100, 100)]
        [TestCase("Premium", 101, 303)]

        [TestCase("AnyOtherValue", 100, 100)]
        public async Task ShouldCreateNormalUser(string userType, decimal money, decimal total)
        {
            var command = new CreateUserCommand
            {
                Name = "Juan",
                Address = "Home 123",
                Email = "Juan@email",
                Money = money,
                Phone = "This is my phone number",
                UserType = userType
            };

            var result = await SendAsync(command);

            var item = await FindAsync<User>(result);

            item.Should().NotBeNull();
            item.Address.Should().Be(command.Address);
            item.Email.Should().Be(command.Email);
            item.Money.Should().Be(total);
            item.Phone.Should().Be(command.Phone);
            item.UserType.Should().Be(command.UserType);
        }
        
        [Test]
        public async Task ShouldRequiereUniqueEmail()
        {
            await SendAsync(new CreateUserCommand
            {
                Name = "Juan 1",
                Address = "Home 1",
                Email = "Juan@email",
                Money = 1,
                Phone = "This is my phone number"                
            });

            var command = new CreateUserCommand
            {
                Name = "Juan 2",
                Address = "Home 2",
                Email = "Juan@email",
                Money = 2,
                Phone = "This is my phone number 2"                
            };

            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<CustomValidationException>();
        }

        [Test]
        public async Task ShouldRequiereUniquePhone()
        {
            await SendAsync(new CreateUserCommand
            {
                Name = "Juan 1",
                Address = "Home 1",
                Email = "Juan1@email",
                Money = 1,
                Phone = "This is my phone number"
            });

            var command = new CreateUserCommand
            {
                Name = "Juan 2",
                Address = "Home 2",
                Email = "Juan2@email",
                Money = 2,
                Phone = "This is my phone number"
            };

            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<CustomValidationException>();
        }

        [Test]
        public async Task ShouldRequiereUniqueNameAndAddress()
        {
            await SendAsync(new CreateUserCommand
            {
                Name = "Juan",
                Address = "Home",
                Email = "Juan1@email",
                Money = 1,
                Phone = "This is my phone number"
            });

            var command = new CreateUserCommand
            {
                Name = "Juan",
                Address = "Home",
                Email = "Juan2@email",
                Money = 2,
                Phone = "This is my phone number 2"
            };

            await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<CustomValidationException>();
        }
    }
}
