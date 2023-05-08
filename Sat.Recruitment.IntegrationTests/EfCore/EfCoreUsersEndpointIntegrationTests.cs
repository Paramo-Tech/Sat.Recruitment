using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Options;
using Sat.Recruitment.Application.Dto.User;
using Sat.Recruitment.Domain.Enum;
using Sat.Recruitment.Infrastructure.SqlServer;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Sat.Recruitment.IntegrationTests.EfCore
{
    public class EfCoreUsersEndpointIntegrationTests : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly SqliteConnection _connection;

        public EfCoreUsersEndpointIntegrationTests()
        {
            string InMemoryConnectionString = "DataSource=:memory:";
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();

            _factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");

                // REMARKS: This is a bit overkill IMHO, but I couldn't find another way to override the configurations from startup.
                builder.ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.EfCoreTest.json");
                });

                builder.ConfigureTestServices(services =>
                {
                    // Unregister existing database service (SQL Server).
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(DbContextOptions<SatRecruitmentDbContext>));

                    if (descriptor != null) services.Remove(descriptor);

                    // Add EF Core Sqlite DB Context.
                    services.AddDbContext<SatRecruitmentDbContext>(options => options.UseSqlite(_connection));
                });

            });

            _httpClient = _factory.CreateClient();
        }

        public void Dispose()
        {
            _factory.Dispose();
            _httpClient.Dispose();

            _connection.Close();
            _connection.Dispose();
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
    }
}
