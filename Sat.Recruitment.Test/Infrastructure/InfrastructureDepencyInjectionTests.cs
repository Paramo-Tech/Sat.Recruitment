using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Infrastructure.SqlServer;
using Sat.Recruitment.Infrastructure.SqlServer.DepencyInjection;
using Sat.Recruitment.Infrastructure.TextFile.Configuration;
using Sat.Recruitment.Infrastructure.TextFile.DepencyInjection;
using Xunit;

namespace Sat.Recruitment.Test.Infrastructure
{
    public class InfrastructureDepencyInjectionTests
    {
        [Fact]
        public void RegisterTextFileInfrastructure_WithoutParameters_RegistersServices()
        {
            // Arrange.
            var services = new ServiceCollection();

            // Act.
            services.RegisterTextFileInfrastructure();
            var provider = services.BuildServiceProvider();

            // Assert.
            Assert.NotNull(provider.GetRequiredService<ITextFileConfiguration>());
        }

        [Fact]
        public void RegisterSqlServerInfrastructure_WithoutParameters_RegistersServices()
        {
            // Arrange.
            var services = new ServiceCollection();

            // Act.
            services.RegisterSqlServerInfrastructure("Server=localhost");
            var provider = services.BuildServiceProvider();

            // Assert.
            Assert.NotNull(provider.GetRequiredService<SatRecruitmentDbContext>());
        }
    }
}
