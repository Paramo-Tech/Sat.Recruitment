using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Domain.Enum;
using Sat.Recruitment.Domain.Model;
using Sat.Recruitment.Infrastructure.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Infrastructure.Repository
{
    public class SqlServerRepositoryTests : IDisposable
    {
        private readonly Mock<ILogger<UserRepository>> _logger;
        private readonly SatRecruitmentDbContext _context;
        private readonly SqliteConnection _connection;

        private static readonly User user1 = new User("Ignacio", "4fake@fake.com", "+5493446121218", "Fake St 3", UserType.Normal, 10);
        private static readonly User user2 = new User("Juan", "fake@fake.com", "+5493446121217", "Fake St 3", UserType.Normal, 10);
        private static readonly User user3 = new User("Julian", "2fake@fake.com", "+5493446121212", "Fake St 3", UserType.Normal, 10);
        private static readonly List<User> Users = new List<User>(){ user1, user2, user3 };

        public SqlServerRepositoryTests()
        {
            _logger = new Mock<ILogger<UserRepository>>();
            
            string InMemoryConnectionString = "DataSource=:memory:";
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            
            var options = new DbContextOptionsBuilder<SatRecruitmentDbContext>()
                    .UseSqlite(_connection)
                    .Options;
            
            _context = new SatRecruitmentDbContext(options);
            _context.Database.EnsureCreated();
            
            using var context = new SatRecruitmentDbContext(options);
            _context.Users.AddRange(Users);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();

            _connection.Close();
            _connection.Dispose();
        }


        [Fact]
        public void GetAll_WithNoParameters_ReturnsList()
        {
            using (_context)
            {                
                // Arrange.
                var repository = new UserRepository(_logger.Object, _context);

                // Act.
                var result = repository.GetAll().OrderBy(x => x.Name);
    
                // Assert.
                Assert.Equal(3, result.Count());
                Assert.Collection(result,
                    elem1 => Assert.Equal(user1.Name, elem1.Name),
                    elem2 => Assert.Equal(user2.Name, elem2.Name),
                    elem3 => Assert.Equal(user3.Name, elem3.Name));
            }
        }

        [Fact]
        public async Task AddAsync_WithValidUser_AddsUser()
        {
            using (_context)
            {
                // Arrange.
                var repository = new UserRepository(_logger.Object, _context);
                var newUser = new User("Test", "test@fake.com", "+5493446121219", "Fake St 3", UserType.Normal, 10);

                // Act.
                var result = await repository.AddAsync(newUser);

                // Assert.                
                Assert.Equal(result.Name, newUser.Name);
                Assert.True(result.UserId > 0);
            }
        }

        [Fact]
        public async Task AddAsync_WithInvalidUser_AddsUser()
        {
            using (_context)
            {
                // Arrange.
                var newUser = new User(null, "test@fake.com", "+5493446121219", null, UserType.Normal, 10);
                var repository = new UserRepository(_logger.Object, _context);

                // Act.
                async Task operation() => await repository.AddAsync(newUser);

                // Assert.                
                await Assert.ThrowsAsync<DbUpdateException>(operation);
            }
        }
    }
}
