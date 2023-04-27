using System.Threading.Tasks;
using NUnit.Framework;
using Sat.Recruitment.Application.Users.Queries;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Enums;
using Shouldly;
using static Sat.Recruitment.Application.IntegrationTests.Testing;

namespace Sat.Recruitment.Application.IntegrationTests.Users.Queries
{
    public class GetUsersQueryTests : BaseTestFixture
    {
        [Test]
        public async Task GetUsersQueryTest()
        {
            // Arrange
            await AddAsync(new User(name: "Juan", email: "Juan@marmol.com", phone: "+5491154762312", address: "Peru 2464", userType: UserTypes.Normal, money: 1234M, isNewUser: true));
            await AddAsync(new User(name: "Franco", email: "Franco.Perez@gmail.com", phone: "+534645213542", address: "Alvear y Colombres", userType: UserTypes.Premium, money: 112234M, isNewUser: true));
            await AddAsync(new User(name: "Agustina", email: "Agustina@gmail.com", phone: "+534645213542", address: "Garay y Otra Calle", userType: UserTypes.SuperUser, money: 112234M, isNewUser: true));

            // Act
            var query = new GetUsersQuery();

            var response = await SendAsync(query);

            // Assert
            response.ShouldNotBeNull();
            response.Count.ShouldBe(3);
        }

    }
}
