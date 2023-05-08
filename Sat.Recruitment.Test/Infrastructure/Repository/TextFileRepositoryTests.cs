using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Domain.Enum;
using Sat.Recruitment.Domain.Model;
using Sat.Recruitment.Infrastructure.TextFile;
using Sat.Recruitment.Infrastructure.TextFile.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Infrastructure.Repository
{
    public class TextFileUserRepositoryTests : IDisposable
    {
        private readonly Mock<ILogger<UserRepository>> _logger;
        private readonly ITextFileConfiguration _configuration;

        private static readonly User user1 = new User("Ignacio", "4fake@fake.com", "+5493446121218", "Fake St 3", UserType.Normal, 10);
        private static readonly User user2 = new User("Juan", "fake@fake.com", "+5493446121217", "Fake St 3", UserType.Normal, 10);
        private static readonly User user3 = new User("Julian", "2fake@fake.com", "+5493446121212", "Fake St 3", UserType.Normal, 10);        

        public TextFileUserRepositoryTests()
        {
            _logger = new Mock<ILogger<UserRepository>>();
            _configuration = new TestFileConfiguration();

            CreateTestFile();
        }

        public void Dispose()
        {
            DeleteTestFile();
        }

        [Fact]
        public void GetAllUsers_WithNoParameters_ReturnsList()
        { 
            // Arrange.           
            var repository = new UserRepository(_logger.Object, _configuration);

            // Act.
            var result = repository.GetAll().OrderBy(x => x.Name);

            // Assert.
            Assert.Equal(3, result.Count());
            Assert.Collection(result,
                elem1 => Assert.Equal(user1.Name, elem1.Name),
                elem2 => Assert.Equal(user2.Name, elem2.Name),
                elem3 => Assert.Equal(user3.Name, elem3.Name));
        }


        [Fact]
        public async Task AddAsync_WithValidUser_AddsUser()
        {
            // Arrange.
            var repository = new UserRepository(_logger.Object, _configuration);
            var newUser = new User("Test", "test@fake.com", "+5493446121219", "Fake St 3", UserType.Normal, 10);

            // Act.
            var result = await repository.AddAsync(newUser);

            // Assert.                
            Assert.Equal(result.Name, newUser.Name);
        }


        private void CreateTestFile()
        {
            // Delete file if exists.
            DeleteTestFile();

            // Create a new file with data that emulates the actual Users.txt file.
            using (StreamWriter outputFile = new StreamWriter(_configuration.TextFilePath()))
            {
                outputFile.WriteLine($"{user1.Name},{user1.Email},{user1.Phone},{user1.Address},{user1.UserType},{user1.Money}");
                outputFile.WriteLine($"{user2.Name},{user2.Email},{user2.Phone},{user2.Address},{user2.UserType},{user2.Money}");
                outputFile.WriteLine($"{user3.Name},{user3.Email},{user3.Phone},{user3.Address},{user3.UserType},{user3.Money}");
            };
        }

        private void DeleteTestFile()
        {
            if (File.Exists(_configuration.TextFilePath()))
            {
                File.Delete(_configuration.TextFilePath());
            }
        }
    }

    internal class TestFileConfiguration : ITextFileConfiguration
    {
        public string TextFilePath()
        {
            return $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Users.txt";
        }
    }
}
