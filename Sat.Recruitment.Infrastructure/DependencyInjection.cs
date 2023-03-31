using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Common.Interfaces.Persistance;
using Sat.Recruitment.Infrastructure.Persistance;

namespace Sat.Recruitment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}