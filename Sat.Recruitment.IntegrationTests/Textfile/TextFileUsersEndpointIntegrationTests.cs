using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api;
using Sat.Recruitment.Application.Dto.User;
using Sat.Recruitment.Domain.Enum;
using Sat.Recruitment.Domain.Model;
using Sat.Recruitment.Infrastructure.TextFile.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Sat.Recruitment.IntegrationTests.EfCore
{
    public class TextFileUsersEndpointIntegrationTests : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly ITextFileConfiguration _configuration;


        private static readonly User user1 = new User("Ignacio", "4fake@fake.com", "+5493446121218", "Fake St 3", UserType.Normal, 10);
        private static readonly User user2 = new User("Juan", "fake@fake.com", "+5493446121217", "Fake St 3", UserType.Normal, 10);
        private static readonly User user3 = new User("Julian", "2fake@fake.com", "+5493446121212", "Fake St 3", UserType.Normal, 10);

        public TextFileUsersEndpointIntegrationTests()
        {
            _configuration = new TestFileConfiguration();

            CreateTestFile();

            _factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");

                // REMARKS: This is a bit overkill IMHO, but I couldn't find another way to override the configurations from startup.
                builder.ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.TextFileTest.json");
                });

                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(service => _configuration);
                });
            });

            _httpClient = _factory.CreateClient();
        }

        public void Dispose()
        {
            _factory.Dispose();
            _httpClient.Dispose();
            DeleteTestFile();
        }

        [Fact]
        public async void CreateUser_WithValidInput_ReturnsOkStatusCode()
        {
            // Arrange.
            var address = "Fake St 123";
            var email = "toofake@fake.com";
            var money = 1M;
            var name = "Jane Doe";
            var phone = "+5493446371436";
            var type = UserType.Normal;

            var request = new CreateUserDto()
            {
                Address = address,
                Email = email,
                Money = money,
                Name = name,
                Phone = phone,
                UserType = type
            };

            var stringPayload = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            // Act.
            var response = await _httpClient.PostAsync($"users", httpContent);

            // Assert.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void CreateUser_WithRepeatedInput_ReturnsBadRequest()
        {
            // Arrange.
            var address = "Fake St 123";
            var email = "fake@fake.com";
            var money = 1M;
            var name = "John Doe";
            var phone = "+5493446371438";
            var type = UserType.Normal;

            var request = new CreateUserDto()
            {
                Address = address,
                Email = email,
                Money = money,
                Name = name,
                Phone = phone,
                UserType = type
            };

            var stringPayload = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            // Act.
            var response = await _httpClient.PostAsync($"users", httpContent);
            var response2 = await _httpClient.PostAsync($"users", httpContent);

            // Assert.
            Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
        }

        [Fact]
        public async void CreateUser_WithInvalidInput_ReturnsBadRequest()
        {
            // Arrange.
            var address = "";
            var email = "";
            var money = 1M;
            var name = "";
            var phone = "";
            var type = UserType.Normal;

            var request = new CreateUserDto()
            {
                Address = address,
                Email = email,
                Money = money,
                Name = name,
                Phone = phone,
                UserType = type
            };

            var stringPayload = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            // Act.
            var response = await _httpClient.PostAsync($"users", httpContent);

            // Assert.
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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
