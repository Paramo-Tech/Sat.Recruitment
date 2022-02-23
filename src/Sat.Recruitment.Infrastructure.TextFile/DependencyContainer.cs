using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Core.Abstractions.Repositories;
using Sat.Recruitment.Infrastructure.TextFile.Repositories;

namespace Sat.Recruitment.Infrastructure.TextFile
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddInfrastructureTextFileDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Repository implementations
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
