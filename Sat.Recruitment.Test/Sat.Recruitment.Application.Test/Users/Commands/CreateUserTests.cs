using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Sat.Recruitment.Application.Common.Exceptions;
using Sat.Recruitment.Application.Users.Commands.Create;
using Sat.Recruitment.Domain.Entities;
using static Sat.Recruitment.Test.Testing;

namespace Sat.Recruitment.Test.Users.Commands
{
    public class CreateUserTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateUserCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();

        }

        [Test]
        public async Task ShouldRequireUniqueUser_UniqueEmail()
        {
            await SendAsync(
                new CreateUserCommand
                {
                     Name  = "Gabriel",
                     Email = "diegogabriel.villafanes@gmail.com",
                     Address = "Obispo Romero 1558 ",
                     Phone = "+543875956083",
                     Money = 10100,
                     UserType = "Normal"
                }
            );

            var command = new CreateUserCommand
            {
                Name = "Jose",
                Email = "diegogabriel.villafanes@gmail.com",
                Address = "Obispo Romero 1551",
                Phone = "+543875956082",
                Money = 10100,
                UserType = "Normal"
            };

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueUser_UniquePhone()
        {
            await SendAsync(
                new CreateUserCommand
                {
                    Name = "Gabriel",
                    Email = "diegogabriel.villafanes@gmail.com",
                    Address = "Obispo Romero 123",
                    Phone = "+543875956083",
                    Money = 10100,
                    UserType = "Normal"
                }
            );

            var command = new CreateUserCommand
            {
                Name = "Jose",
                Email = "diegogabriel.villafanes2@gmail.com",
                Address = "Obispo Romero 1234",
                Phone = "+543875956083",
                Money = 10100,
                UserType = "Normal"
            };

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueUser_SameNameAndAddress()
        {
            await SendAsync(
                new CreateUserCommand
                {
                    Name = "Jose",
                    Email = "diegogabriel.villafanes@gmail.com",
                    Address = "Obispo Romero 123",
                    Phone = "+543875956083",
                    Money = 10100,
                    UserType = "Normal"
                }
            );

            var command = new CreateUserCommand
            {
                Name = "Jose",
                Email = "diegogabriel.villafanes2@gmail.com",
                Address = "Obispo Romero 123",
                Phone = "+543875956083",
                Money = 10100,
                UserType = "Normal"
            };

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }


        [Test]
        public async Task ShouldCreateUser()
        {

            var command = new CreateUserCommand
            {
                Name = "Diego",
                Email = "diegogabriel.villafanes@gmail.com",
                Address = "Obispo Romero 1551",
                Phone = "+543875956082",
                Money = 10100,
                UserType = "Normal"
            };
            
            var result = await SendAsync(command);

            var list = await FindAsync<User>(result.Data.Id);

            list.Should().NotBeNull();
            list.Name.Should().Be(command.Name);
        }

        [Test]
        public async Task ShouldCreateUser_SameName()
        {
            await SendAsync(
                new CreateUserCommand
                {
                    Name = "Jose Dario",
                    Email = "diegogabriel.villafanes@gmail.com",
                    Address = "Obispo Romero 123",
                    Phone = "+543875956083",
                    Money = 10100,
                    UserType = "Normal"
                }
            );

            var command = new CreateUserCommand
            {
                Name = "Jose Dario",
                Email = "diegogabriel.villafanes2@gmail.com",
                Address = "Obispo Romero 1234",
                Phone = "+543875956084",
                Money = 10100,
                UserType = "Normal"
            };

            var result = await SendAsync(command);
            var list = await FindAsync<User>(result.Data.Id);

            list.Should().NotBeNull();
            list.Name.Should().Be(command.Name);
        }



    }
}
