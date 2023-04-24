using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.DTOs.Enums;
using Sat.Recruitment.DTOs.Models;
using Sat.Recruitment.EF.Context;
using Sat.Recruitment.Services.Commands.Imp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Commands
{
    public class GetUsersCommandTest
    {
        private readonly DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "GetUsersDatabase")
        .Options;

        [Fact]
        public async Task Execute_ReturnsAllUsers()
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Users.AddRange(new List<User>
                {
                    new User { Name = "Pablo", Email = "pablo@gmail.com", Phone = "1234567890", Address = "Illia 50", UserType = UserType.SuperUser, Money =150},
                    new User { Name = "Juan", Email = "juan@gmail.com", Phone = "0987654321", Address = "Independencia 100", UserType = UserType.Premium, Money =500 }
                });

                context.SaveChanges();

                var command = new GetUsersCommand(context);

                var result = await command.Execute();

                Assert.Equal(2, result.Count);
            }
        }
    }
}