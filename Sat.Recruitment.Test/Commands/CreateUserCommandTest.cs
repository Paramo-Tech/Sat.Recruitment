using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.DTOs.Enums;
using Sat.Recruitment.DTOs.Requests;
using Sat.Recruitment.EF.Context;
using Sat.Recruitment.Services.Commands.Imp;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Commands
{
    public class CreateUserCommandTest
    {
        private readonly DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "CreateUserDatabase")
            .Options;

        [Fact]
        public async Task Execute_ShouldCreateUserInDatabase()
        {
            var request = new UserCreateRequest
            {
                Name = "Pablo Perez",
                Email = "pperez@gmail.com",
                Address = "Avellaneda 123",
                Phone = "3512098123",
                UserType = UserType.Normal,
                Money = 150
            };
            using (var context = new ApplicationDbContext(options))
            {
                var command = new CreateUserCommand(context);

                var result = await command.Execute(request);

                Assert.True(result.Success);

                var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                Assert.NotNull(user);
            }
        }
    }
}