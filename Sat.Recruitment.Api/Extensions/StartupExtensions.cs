using System;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Infrastructure;

namespace Sat.Recruitment.Api.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencies(configuration);
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            //services.AddMySql(configuration);
            services.AddScoped<IUserRepository, FileUserRepository>();

            return services;
        }
    }
}

