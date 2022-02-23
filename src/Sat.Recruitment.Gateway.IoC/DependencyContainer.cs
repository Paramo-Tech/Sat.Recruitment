using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Core;
using Sat.Recruitment.Infrastructure.EF;
using Sat.Recruitment.Infrastructure.TextFile;

namespace Sat.Recruitment.Gateway.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddGatewayDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Infrastructure dependencies
            services.AddInfrastructureTextFileDependencies(configuration);
            //services.AddInfrastructureEFDependencies(configuration);

            // Core dependencies
            services.AddCoreDependencies(configuration);

            return services;
        }
    }
}
