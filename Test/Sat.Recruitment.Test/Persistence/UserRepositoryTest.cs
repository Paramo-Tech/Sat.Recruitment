using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace Sat.Recruitment.Test.Persistence
{
    public class UserRepositoryTest
    {
        ApplicationDbContext ent;

        [Fact]
        public async Task CreateUserTestAsync()
        {
            InitContext();

            var repository = new UserRepository(ent);
            var result = await repository.AddUser(new User
            {
                Address = "Test",
                Email = "Test@test.com",
                Money = 100,
                Name = "Test",
                Phone = "300214541",
                UserId =0,
                UserType = 1
            });

           Assert.NotNull(result);
        }


        [Fact]
        public async Task GetUserTestAsync()
        {
            InitContext();

            var repository = new UserRepository(ent);
            var result = await repository.GetAllAsync();

            Assert.NotNull(result);
        }

        private void InitContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "dbcontext");

            var context = new ApplicationDbContext(builder.Options);
            var list = Enumerable.Range(1, 10)
                .Select(i => new User
                {
                    Address = "Test",
                    Email = "Test@test.com",
                    Money = 100,
                    Name = "Test",
                    Phone = "300214541",
                    UserId = i,
                    UserType = 1
                });

            context.User.AddRange(list);
            try { int changed = context.SaveChanges(); } catch (Exception) { }
            ent = context;
        }

    }
}
