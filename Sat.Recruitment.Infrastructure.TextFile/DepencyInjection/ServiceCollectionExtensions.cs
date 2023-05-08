using Sat.Recruitment.Infrastructure.TextFile.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sat.Recruitment.Infrastructure.TextFile.DepencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all required services and classes to operate with TextFile as the persistence provider..
        /// </summary>
        /// <param name="services">Instance of the ServiceCollection where the services will registered.</param>
        public static void RegisterTextFileInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ITextFileConfiguration, TextFileConfiguration>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
