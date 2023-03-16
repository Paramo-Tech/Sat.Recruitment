using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Sat.Recruitment.Application.Users.Commands.Create;
using Sat.Recruitment.Application.Users.Queries.GetUsers;
using Sat.Recruitment.Test;
using FluentAssertions.Extensions;
using static Sat.Recruitment.Test.Testing;

namespace Sat.Recruitment.Test.Users.Commands.Queries
{
    public class GetAllUsersTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllUsres()
        {
           var user = await SendAsync(
                new CreateUserCommand
                {
                     Name  = "Diego ",
                     Email = "diegogabriel.villafanes@gmail.com",
                     Address = "Obispo Romero 1551",
                     Phone = "+543875956082",
                     Money = 10100,
                     UserType = "Normal"
                }
            );

            var query = new GetAllUsersQuery();

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Count.Should().BeGreaterThan(0);
        }
    }
}