using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Core;
using Sat.Recruitment.Core.Abstractions.Repositories;
using Sat.Recruitment.Infrastructure.Repositories;

namespace Sat.Recruitment.Gateway.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddGatewayDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Infrastructure dependencies
            services.AddTransient<IUserRepository, UserRepository>();

            // Core dependencies
            services.AddCoreDependencies(configuration);

            return services;
        }
    }
}
